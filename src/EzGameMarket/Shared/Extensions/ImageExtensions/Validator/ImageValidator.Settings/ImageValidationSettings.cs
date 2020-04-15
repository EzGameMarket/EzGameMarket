using Shared.Extensions.ImageExtensions.ImageValidator.Settings.Abstractions;
using Shared.Extensions.ImageExtensions.ImageValidator.Settings.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Extensions.ImageExtensions.ImageValidator.Settings
{
    public class ImageValidationSettings : IImageValidateSettings
    {
        public IEnumerable<string> ValidExtensions { get; set; }
        public IEnumerable<ValidDimensionViewModel> ValidDimensions { get; set; }
    }
}
