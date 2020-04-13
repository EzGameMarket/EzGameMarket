using Shared.Utilities.CloudStorage.Settings.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Shared.Utilities.CloudStorage.Settings.Services
{
    public class StorageSettingsService<TSettings> : IStorageSettingsService<TSettings> 
        where TSettings : IStorageSettings
    {
        public StorageSettingsService(string filePath = "storageConfiguration.json")
        {
            SettingsFilePath = filePath;
        }

        public string SettingsFilePath { get; set; }

        public async Task<TSettings> Load()
        {
            if (File.Exists(SettingsFilePath) == false)
            {
                throw new FileNotFoundException($"A {SettingsFilePath} cloudStroage settings fájl nem létezik");
            }

            var jsonText = await File.ReadAllTextAsync(SettingsFilePath);

            return JsonConvert.DeserializeObject<TSettings>(jsonText);
        }

        public async Task<bool> Rewrite(TSettings model)
        {
            if (File.Exists(SettingsFilePath) == false)
            {
                throw new FileNotFoundException($"A {SettingsFilePath} cloudStroage settings fájl nem létezik");
            }

            var jsonText = JsonConvert.SerializeObject(model);

            await File.WriteAllTextAsync(SettingsFilePath, jsonText);

            return true;
        }
    }
}
