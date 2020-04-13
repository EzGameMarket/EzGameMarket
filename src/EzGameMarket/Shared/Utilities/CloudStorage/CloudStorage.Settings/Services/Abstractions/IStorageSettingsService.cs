using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.CloudStorage.Settings.Services.Abstractions
{
    public interface IStorageSettingsService<TSettings> where TSettings : IStorageSettings
    {
        string SettingsFilePath { get; set; }

        Task<bool> Rewrite(TSettings model);

        Task<TSettings> Load();
    }
}
