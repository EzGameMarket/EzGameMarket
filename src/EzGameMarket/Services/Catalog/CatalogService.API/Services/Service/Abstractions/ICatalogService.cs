using CatalogService.API.Models;
using CatalogService.API.ViewModels.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogService.API.Services.Service.Abstractions
{
    public interface ICatalogService
    {
        Task<ProductViewModel> GetProductAsync(string productID);

        Task<List<CatalogItem>> GetItemsAsync(int skip, int take);
        Task<List<CatalogItem>> GetItemsFromIDsAsync(IEnumerable<string> ids);

        Task<long> GetAllItemsCount();
    }
}