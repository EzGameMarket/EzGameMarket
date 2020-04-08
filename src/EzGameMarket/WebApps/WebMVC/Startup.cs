using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Shared.Extensions.HttpClientHandler;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using WebMVC.Extensions.Settings;
using WebMVC.Services.Repositorys.Abstractions;
using WebMVC.Services.Repositorys.Implementation;
using WebMVC.Services.Services.Abstractions;
using WebMVC.Services.Services.Implementation;

namespace WebMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddRazorPages();

            services.AddDistributedMemoryCache();
            AddJWT(services);

            services.AddHttpClient<IHttpHandlerUtil, HttpHandlerUtil>((serviceProvider, c) => 
            {
                // Find the HttpContextAccessor service
                var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
                // Get the bearer token from the request context (header)
                var bearerToken = httpContextAccessor.HttpContext.Request
                                      .Headers["Authorization"]
                                      .FirstOrDefault(h => h.StartsWith("bearer ", StringComparison.InvariantCultureIgnoreCase));

                // Add authorization if found
                if (bearerToken != null)
                    c.DefaultRequestHeaders.Add("Authorization", bearerToken);

                // Other settings
                c.DefaultRequestHeaders.Add("Accept", "application/json");
                c.DefaultRequestHeaders.Add("User-Agent", "EzgameMarket-WEBMVC-UserAgent");

            }).SetHandlerLifetime(TimeSpan.FromMinutes(5));

            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<ICatalogRepository, CatalogRepository>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddSingleton<ILoadBalancerUrls, LoadBalancerUrls>();
        }

        private void AddJWT(IServiceCollection services)
        {
            var key = Encoding.UTF8.GetBytes(Configuration.GetValue<string>("Secret"));

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    AuthenticationType = "Identity.Application"
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePagesWithRedirects("/Error/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = ctx =>
                {
                    // Cache static files for 30 days
                    ctx.Context.Response.Headers.Add("Cache-Control", "public,max-age=2592000");
                    ctx.Context.Response.Headers.Add("Expires", DateTime.UtcNow.AddDays(30).ToString("R", CultureInfo.InvariantCulture));
                }
            });

            app.UseRouting();

            app.Use(async (context, next) =>
            {
                var authenticationCookieName = "access_token";
                var cookie = context.Request.Cookies[authenticationCookieName];
                if (cookie != null)
                {
                    if (context.Request.Headers.ContainsKey("Authorization") == false)
                    {
                        context.Request.Headers.Add("Authorization", "Bearer " + cookie);
                    }
                }

                await next();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}