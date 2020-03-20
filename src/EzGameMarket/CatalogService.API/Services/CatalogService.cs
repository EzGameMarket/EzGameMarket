using CatalogService.API.Data;
using CatalogService.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.Services
{
    public class CatalogService : ICatalogService
    {
        private ProductDbContext _dbContext;

        public CatalogService(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Product> GetProductAsync(string id)
        {
            return _dbContext.Products.FirstOrDefaultAsync(p => p.GameID == id);
        }

        public Task<List<Product>> GetProductForGenreAsync(Genre type)
        {
            return _dbContext.Products.Where(p=> p.Genres.Contains(type)).ToListAsync();
        }

        public Task<List<Product>> GetProductsForPlatformAsync(Platform platform)
        {
            return _dbContext.Products.Where(p => p.Platforms.Contains(platform)).ToListAsync();
        }

        public Task<List<Product>> GetProductsForRegionAsync(Region region)
        {
            return _dbContext.Products.Where(p => p.Regions.Contains(region)).ToListAsync();
        }

        public Task<List<Product>> GetRandomProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
