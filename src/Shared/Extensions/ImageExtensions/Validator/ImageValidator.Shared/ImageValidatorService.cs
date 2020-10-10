using Microsoft.AspNetCore.Http;
using Shared.Extensions.ImageExtensions.ImageValidator.Settings;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Extensions.ImageExtensions.ImageValidator.Shared
{
    public class ImageValidatorService : IImageValidatorService
    {
        public ImageValidatorService(ImageValidationSettings validateSettings)
        {
            ValidateSettings = validateSettings;
        }

        public ImageValidationSettings ValidateSettings { get; private set; }

        public Task<bool> ValidateDimensions(IFormFile file, Size min, Size max)
        {
            using var stream = file.OpenReadStream();
            using var image = Image.Load(stream, out IImageFormat format);

            return Task.FromResult(image.Height >= min.Height &&
                                   image.Height <= max.Height &&
                                   image.Width >= min.Width &&
                                   image.Width <= max.Width);
        }        
        
        public Task<bool> ValidateExtensions(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);

            return Task.FromResult(ValidateSettings.ValidExtensions.Contains(extension));
        }

        public Task<bool> ValidateSize(IFormFile image, int max)
        {
            if (image.Length >= max)
            {
                throw new ApplicationException($"A {image.FileName} fájl mérete meghaladta a {Math.Round(max / 1_048_576.0, 2)} MB-ot");
            }

            return Task.FromResult(true);
        }
    }
}
