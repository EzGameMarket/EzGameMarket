using CatalogImages.API.Models;
using CatalogImages.API.ViewModels.Image;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogImages.API.Services.Service.Abstractions
{
    public interface ICatalogItemImageService
    {

        Task<List<CatalogItemImageModel>> GetAllImageForProductID(string productID);
        Task<List<CatalogItemImageModel>> GetAllImageForProductIDByFiltering(string productID, string typeName = default, string sizeName = default);

        Task UploadImage(string productID, string fileNameWithExtension, IFormFile file);
        Task UploadImage(string fileNameWithExtension, IFormFile file);


        Task DeleteImage(string fileNameWithExtension);

        Task AddNewImage(AddNewImageViewModel model);
        Task ModifyImage(int id,ModifyImageViewModel model);

        Task RemoveImage(int id);
    }
}
