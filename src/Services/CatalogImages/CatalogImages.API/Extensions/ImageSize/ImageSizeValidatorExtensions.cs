using CatalogImages.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogImages.API.Extensions.ImageSize
{
    public static class ImageSizeValidatorExtensions
    {
        public static bool Validate(this ImageSizeModel model) => 
                    string.IsNullOrEmpty(model.Name) == false
                    && model.Width.IsInRange(ImageSizeModel.MinWidth, ImageSizeModel.MaxWidth)
                    && model.Height.IsInRange(ImageSizeModel.MinHeight, ImageSizeModel.MaxHeight);
    }
}
