using Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.ViewModels.Pagination;
using WebMVC.ViewModels.Products;

namespace WebMVC.Services.Repositorys.Abstractions
{
    public interface ICatalogRepository
    {
        Task<PaginationViewModel<ProductItem>> GetItems(int skip, int take);
        Task<PaginationViewModel<ProductItem>> GetItemsFromCategories(int skip, int take, IEnumerable<string> categories);
        Task<PaginationViewModel<ProductItem>> GetItemsFromTags(int skip, int take, IEnumerable<string> tags);
        Task<PaginationViewModel<ProductItem>> GetItemsFromTagsAndCategories(int skip, int take, IEnumerable<string> tags, IEnumerable<string> categories);

        Task<PaginationViewModel<ProductItem>> FilterItems(int skip, int take, IEnumerable<string> tags = default, IEnumerable<string> categories = default);

        Task<ProductItem> GetProduct(string productID);

        Task<Product> GetProductDetail(string productID);

        Task<IEnumerable<ProductItem>> GetRecomendedProducts(string userID);
    }
}
