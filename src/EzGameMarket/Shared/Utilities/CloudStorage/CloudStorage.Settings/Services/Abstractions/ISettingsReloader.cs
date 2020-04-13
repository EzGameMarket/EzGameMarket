using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.CloudStorage.Settings.Services.Abstractions
{
    public interface ISettingsReloader
    {
        string SettingsFilePath { get; set; }
        bool ReloadOnChange { get; set; }

        IStorageSettings Settings { get; set; }

        Task Watch();

        Task Reload();
    }
}
