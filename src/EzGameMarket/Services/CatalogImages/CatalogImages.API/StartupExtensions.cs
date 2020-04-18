using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Utilities.CloudStorage.Shared.Services.Abstractions;
using Shared.Utilities.CloudStorage.Shared;
using Shared.Utilities.CloudStorage.AzureBlob;
using Shared.Utilities.CloudStorage.AzureBlob.Settings;
using Shared.Extensions.SettingsLoader.Services.Abstractions;
using Shared.Extensions.SettingsLoader.Services.Implementations;

namespace CatalogImages.API
{
    public static class StartupExtensions
    {
        public static async Task AddCloudStorage(this IServiceCollection services)
        {
            var storageSettings = new SettingsService<AzureSettings>("cloudStorageSettings.json");
            var settings = await storageSettings.LoadAsync();

            services.AddSingleton(settings);
            services.AddSingleton<IStorageRepository, AzureBlobStorage>();
            services.AddSingleton<IStorageService,StorageService>();
        }

        public static void AddJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.UTF8.GetBytes(configuration.GetValue<string>("Secret"));

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
    }
}
