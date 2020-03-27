using CatalogService.API.Models;
using System.Collections.Generic;
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

        Task<List<Product>> GetItemsAsync(int skip, int take);

        Task<long> GetAllItemsCount();
    }
}