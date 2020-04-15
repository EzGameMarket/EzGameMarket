using Shared.Extensions.ImageExtensions.ImageValidator.Settings.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Extensions.ImageExtensions.ImageValidator.Settings.Abstractions
{
    public interface IImageValidateSettings
    {
        IEnumerable<string> ValidExtensions { get; set; }
        IEnumerable<ValidDimensionViewModel> ValidDimensions { get; set; }
    }
}
