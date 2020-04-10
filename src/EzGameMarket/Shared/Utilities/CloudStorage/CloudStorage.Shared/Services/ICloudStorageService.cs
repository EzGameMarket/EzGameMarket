using Shared.Utiliies.CloudStorage.Settings.Services;
using Shared.Utiliies.CloudStorage.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utiliies.CloudStorage.Shared.Services
{
    public interface ICloudStorageService
    {
        IStorageRepository Repository { get; set; }

        IStorageSettingsService Settings { get; set; }

        Task Init();


    }
}
