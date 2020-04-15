using Shared.Extensions.ImageExtensions.ImageValidator.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Extensions.ImageExtensions.ImageValidator.Settings.Service.Abstractions
{
    public interface IImageValidatorSettingsLoader
    {
        string FilePath { get; set; }

        Task<IImageValidateSettings> LoadAsync();
        Task WriteAsync(IImageValidateSettings model);
    }
}
