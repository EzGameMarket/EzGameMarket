using CatalogImages.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogImages.API.Services.Repositories.Abstractions
{
    public interface IImageTypeRepository
    {
        Task<ImageTypeModel> GetByID(int id);
        Task<ImageTypeModel> GetByIDWithImages(int id);

        Task<List<ImageTypeModel>> GetAllTypes();


        Task<bool> AnyWithID(int id);
        Task<bool> AnyWithName(string name);

        Task Modify(int id, ImageTypeModel model);

        Task Add(ImageTypeModel model);

        Task Delete(int id);
    }
}
