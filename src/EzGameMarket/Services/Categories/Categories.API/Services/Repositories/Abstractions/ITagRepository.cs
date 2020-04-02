using Categories.API.Models;
using Categories.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Categories.API.Services.Repositories.Abstractions
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetTagsForProduct(string productID);

        Task<Tag> GetTag(int tagID);

        Task<List<string>> GetProductsForTagID(int tagID);

        Task AddNewTag(Tag newTag);
        Task Modify(int tagID,Tag model);

        Task AddProductToTag(NewProductToTagViewModel model);
    }
}
