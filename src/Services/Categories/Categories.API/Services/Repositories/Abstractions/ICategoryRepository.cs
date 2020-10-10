using Categories.API.Models;
using Categories.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Categories.API.Services.Repositories.Abstractions
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategoriesForProduct(string productID);

        Task<Category> GetCategory(int categoryID);

        Task<List<string>> GetProductsForCategoryID(int categoryID);

        Task AddNewCategory(Category newCategory);
        Task Modify(int categoryID, Category model);

        Task AddProductToCategories(NewProductToCategoryViewModel model);
    }
}
