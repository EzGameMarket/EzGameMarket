using CatalogService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.Services
{
    public interface ICatalogService
    {
        Task<List<Product>> GetRandomProductsAsync();

        Task<List<Product>> GetProductForGenreAsync(Genre type);

        Task<List<Product>> GetProductsForRegionAsync(Region region);

        Task<List<Product>> GetProductsForPlatformAsync(Platform platform);

        Task<Product> GetProductAsync(string id);
    }
}
