using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Polly;
using Ocelot.Cache.CacheManager;
using Ocelot.Provider.Eureka;
using Web.Gtw.Infrastructare.ServiceAccess.Abstractions;
using Web.Gtw.Infrastructare.ServiceAccess;
using Newtonsoft.Json;
using Shared.Extensions.HttpClientHandler;
using Web.Gtw.Services.Repositories.Abstractions;
using Web.Gtw.Services.Repositories.Implementation;
using Microsoft.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Shared.Services.IdentityConverter.Abstractions;
using Shared.Services.IdentityConverter;

namespace Web.Gtw
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
            services.AddControllers();
            services.AddOcelot().AddPolly().AddCacheManager(x =>
            {
                x.WithDictionaryHandle();
            })
            .AddEureka();
            AddJWT(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddHttpContextAccessor();

            var urls = JsonConvert.DeserializeObject<ServiceUrls>(System.IO.File.ReadAllText("services.json"));

            services.AddSingleton<IServiceUrls, ServiceUrls>(s=> {
                return new ServiceUrls() 
                {
                    Cart = urls.Cart,
                    Catalog = urls.Catalog,
                    Identity = urls.Identity,
                    Order = urls.Order
                };
            });

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
                c.DefaultRequestHeaders.Add("User-Agent", "EzgameMarket-Web-GTW-UserAgent");

            }).SetHandlerLifetime(TimeSpan.FromMinutes(5));

            services.AddScoped<ICartRepository,CartRepository>();
            services.AddScoped<ICatalogRepository,CatalogRepository>();
            services.AddScoped<IIdentityConverterService, IdentityConverterService>();
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

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
