using CatalogImages.API.Exceptions.ImageType.Model;
using CatalogImages.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogImages.API.ViewModels.Image
{
    public class AddNewImageViewModel
    {
        public AddNewImageViewModel(string productID, ImageTypeModel type, ImageSizeModel size, string imageURI)
        {
            ProductID = productID;
            Type = type;
            Size = size;
            ImageURI = imageURI;
        }

        public string ProductID { get; set; }
        public ImageTypeModel Type { get; set; }
        public ImageSizeModel Size { get; set; }
        public string ImageURI { get; set; }
    }
}
