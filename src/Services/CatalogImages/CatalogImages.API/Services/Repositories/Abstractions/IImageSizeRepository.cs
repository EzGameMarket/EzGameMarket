using CatalogImages.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogImages.API.Services.Repositories.Abstractions
{
    public interface IImageSizeRepository
    {
        Task<ImageSizeModel> GetByID(int id);
        Task<ImageSizeModel> GetByIDWithImages(int id);

        Task<List<ImageSizeModel>> GetAllSizes();


        Task<bool> AnyWithID(int id);
        Task<bool> AnyWithName(string name);

        Task Modify(int id, ImageSizeModel model);

        Task Add(ImageSizeModel model);

        Task Delete(int id);
    }
}
