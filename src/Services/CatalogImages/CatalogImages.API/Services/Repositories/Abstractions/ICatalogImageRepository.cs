using CatalogImages.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogImages.API.Services.Repositories.Abstractions
{
    public interface ICatalogImageRepository
    {
        Task<CatalogItemImageModel> GetByID(int id);
        Task<bool> AnyWithID(int id);
        Task<bool> AnyWithUrl(string url);

        Task Add(CatalogItemImageModel model);
        Task Delete(int id);

        Task Modify(int id, CatalogItemImageModel model);
        Task ModifySize(int id, int sizeID);
        Task ModifyType(int id, int typeID);
    }
}
