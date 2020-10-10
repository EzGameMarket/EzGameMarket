using CatalogService.API.Data;
using CatalogService.API.Extensions;
using CatalogService.API.Models;
using CatalogService.API.Services.Service.Abstractions;
using CatalogService.API.ViewModels.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.Services.Service.Implementations
{
    public class CatalogServiceProvider : ICatalogService
    {
        private ProductDbContext _dbContext;

        public CatalogServiceProvider(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<long> GetAllItemsCount()
        {
            return _dbContext.Products.LongCountAsync();
        }

        public async Task<List<CatalogItem>> GetItemsAsync(int skip, int take)
        {
            var data = await _dbContext.Products.Skip(skip).Take(take).ToListAsync();

            return data.Select(p => p.ToCatalogItem()).ToList();
        }

        public async Task<List<CatalogItem>> GetItemsFromIDsAsync(IEnumerable<string> ids)
        {
            var data = await _dbContext.Products.Where(p => ids.Contains(p.GameID)).ToListAsync();

            return data.Select(p => p.ToCatalogItem()).ToList();
        }

        public async Task<ProductViewModel> GetProductAsync(string productID)
        {
            var model = await _dbContext.Products.FirstOrDefaultAsync(p=> p.GameID == productID || p.ID == productID);

            return new ProductViewModel();
        }
    }
}