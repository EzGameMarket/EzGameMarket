using Shared.Extensions.ImageExtensions.ImageValidator.Settings.Abstractions;
using Shared.Extensions.ImageExtensions.ImageValidator.Settings.Service.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Extensions.ImageExtensions.ImageValidator.Settings.Service
{
    public class ImageValidationSettingsLoader : IImageValidatorSettingsLoader
    {
        public ImageValidationSettingsLoader(string filePath)
        {
            FilePath = filePath;
        }

        public string FilePath { get; set; }

        public async Task<IImageValidateSettings> LoadAsync()
        {
            CheckFilePathValidation();

            var text = await File.ReadAllTextAsync(FilePath);

            //return JsonConvert.DeserializeObject<IImageValidateSettings>(text);

            return default;
        }

        public async Task WriteAsync(IImageValidateSettings model)
        {
            CheckFilePathValidation();

            //var jsonText = JsonConvert.SerializeObject(model);

            //await File.WriteAllTextAsync(FilePath, jsonText);
        }

        private void CheckFilePathValidation()
        {
            if (File.Exists(FilePath) == false)
            {
                throw new FileNotFoundException($"A {FilePath} fájl nem létezik az kép validáció beállításaihoz");
            }
        }
    }
}
