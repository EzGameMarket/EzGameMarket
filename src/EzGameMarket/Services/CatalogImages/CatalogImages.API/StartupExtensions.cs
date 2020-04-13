using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Utilities.CloudStorage.Settings.Services.Abstractions;
using Shared.Utilities.CloudStorage.Settings.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Utilities.CloudStorage.Shared.Services.Abstractions;
using Shared.Utilities.CloudStorage.Shared;
using Shared.Utilities.CloudStorage.AzureBlob;
using Shared.Utilities.CloudStorage.AzureBlob.Settings;

namespace CatalogImages.API
{
    public static class StartupExtensions
    {
        public static async Task AddCloudStorage(this IServiceCollection services)
        {
            var storageSettings = new StorageSettingsService<AzureSettings>();
            var settings = await storageSettings.Load();

            services.AddSingleton<IStorageSettings>(settings);
            services.AddSingleton<IAzureSettings>(settings);
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
