using Microsoft.AspNetCore.Http;
using Shared.Extensions.ImageExtensions.ImageValidator.Settings.Abstractions;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Extensions.ImageExtensions.ImageValidator.Shared
{
    public interface IImageValidatorService
    {
        ImageValidateSettings ValidateSettings { get; }

        //meg kell oldani, hogy a feltölött fájl méretét lehessen itt megvizsgálni
        Task<bool> ValidateSize(IFormFile file, int max);

        //meg kell oldani, hogy a feltölött fájl dimenzióit
        Task<bool> ValidateDimensions(IFormFile file, Size min, Size max);
        Task<bool> ValidateExtensions(IFormFile file);
    }
}
