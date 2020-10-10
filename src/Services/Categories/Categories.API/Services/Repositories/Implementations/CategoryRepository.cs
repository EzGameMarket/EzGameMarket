using Categories.API.Data;
using Categories.API.Exceptions;
using Categories.API.Models;
using Categories.API.Services.Repositories.Abstractions;
using Categories.API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Categories.API.Services.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private CategoryDbContext _dbContext;

        public CategoryRepository(CategoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddNewCategory(Category newCategory)
        {
            if (newCategory.ID.HasValue)
            {
                var tag = await GetCategory(newCategory.ID.Value);

                if (tag != default)
                {
                    throw new CategoryAlreadyInDataBaseException() { CategoryID = newCategory.ID.Value, CategoryName = newCategory.Name };
                }
            }

            newCategory.ID = default;
            newCategory.Products = new List<CategoriesAndProductsLink>();

            await _dbContext.Categories.AddAsync(newCategory);

            await _dbContext.SaveChangesAsync();
        }

        public async Task AddProductToCategories(NewProductToCategoryViewModel model)
        { 
            var category = await GetCategory(model.CategoryID);

            if (category == default)
            {
                throw new CategoryNotFoundInDataBaseException() { CategoryID = model.CategoryID };
            }

            if (category.Products == default)
            {
                category.Products = new List<CategoriesAndProductsLink>();
            }

            category.Products.Add(new CategoriesAndProductsLink() { ProductID = model.ProductID });

            await _dbContext.SaveChangesAsync();
        }

        public Task<List<Category>> GetCategoriesForProduct(string productID) => _dbContext.Categories.Include(c=> c.Products).Where(c => c.Products.Any(p => p.ProductID == productID)).ToListAsync();

        public Task<Category> GetCategory(int categoryID) => _dbContext.Categories.Include(c => c.Products).FirstOrDefaultAsync(c=> c.ID == categoryID);

        public async Task<List<string>> GetProductsForCategoryID(int categoryID)
        {
            var category = await GetCategory(categoryID);

            if (category != default)
            {
                return category.Products.Select(p=> p.ProductID).ToList();
            }
            else
            {
                throw new CategoryNotFoundInDataBaseException() { CategoryID = categoryID };
            }
        }

        public async Task Modify(int categoryID, Category model)
        {
            var category = await GetCategory(categoryID);

            if (category == default)
            {
                throw new CategoryNotFoundInDataBaseException() { CategoryID = categoryID };
            }

            _dbContext.Entry(category).CurrentValues.SetValues(model);

            await _dbContext.SaveChangesAsync();
        }
    }
}
