using Shared.Utilities.CloudStorage.Settings.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.CloudStorage.Settings.Services
{
    public class SettingsReloader<TSettings> where TSettings : IStorageSettings
    {
        public SettingsReloader(IStorageSettingsService<TSettings> storageSettingsService,
                                TSettings settings,
                                bool reloadOnChange = true)
        {
            ReloadOnChange = reloadOnChange;
            StorageSettingsService = storageSettingsService;
            Settings = settings;
        }

        public bool ReloadOnChange { get; set; }
        public IStorageSettingsService<TSettings> StorageSettingsService { get; set; }
        public TSettings Settings { get; set; }

        public Task Reload()
        {
            throw new NotImplementedException();
        }

        public Task Watch()
        {
            throw new NotImplementedException();
        }
    }
}
