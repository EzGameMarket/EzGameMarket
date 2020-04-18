using Newtonsoft.Json;
using Shared.Extensions.SettingsLoader.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions.SettingsLoader.Services.Implementations
{
    public class SettingsService<TSettings> : ISettingsService<TSettings>
        where TSettings : class
    {
        public string FilePath { get; set; }

        private void ValidateFileHandling()
        {
            if (File.Exists(FilePath) == false)
            {
                throw new FileNotFoundException($"A {FilePath} fájl nem létezik");
            }
        }

        public TSettings Load()
        {
            ValidateFileHandling();

            var jsonText = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<TSettings>(jsonText);
        }

        public async Task<TSettings> LoadAsync()
        {
            ValidateFileHandling();

            var jsonText = await File.ReadAllTextAsync(FilePath);
            return JsonConvert.DeserializeObject<TSettings>(jsonText);
        }

        public bool Save(TSettings data)
        {
            ValidateFileHandling();

            var jsonText = JsonConvert.SerializeObject(data);
            File.WriteAllText(FilePath, jsonText);
            return true;
        }

        public async Task<bool> SaveAsync(TSettings data)
        {
            ValidateFileHandling();

            var jsonText = JsonConvert.SerializeObject(data);
            await File.WriteAllTextAsync(FilePath, jsonText);
            return true;
        }
    }
}
