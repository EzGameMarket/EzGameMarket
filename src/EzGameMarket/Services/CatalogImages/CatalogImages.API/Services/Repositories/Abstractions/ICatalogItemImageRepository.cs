using CatalogImages.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogImages.API.Services.Repositories.Abstractions
{
    public interface ICatalogItemImageRepository
    {

        Task<List<CatalogItemImageModel>> GetAllImageForProductID(string productID);
        Task<List<CatalogItemImageModel>> GetAllImageForProductIDByFiltering(string productID, string typeName = default, string sizeName = default);

        Task AddNewImage(CatalogItemImageModel model);

        Task RemoveImage(int id);
    }
}
