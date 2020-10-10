using CatalogImages.API.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogImages.API.ViewModels.Image
{
    public class UploadNewImageViewModel
    {
        [Required]
        public string ProductID { get; set; }

        [Required]
        public ImageTypeModel Type { get; set; }

        [Required]
        public ImageSizeModel Size { get; set; }

        [Required]
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
