using Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models.Pagination;
using WebMVC.Models.Products;

namespace WebMVC.Services.Repositorys.Abstractions
{
    public interface ICatalogRepository
    {
        Task<PaginationViewModel<ProductItem>> GetItems(int skip, int take);
        Task<PaginationViewModel<ProductItem>> GetItemsFromBrands(int skip, int take, IEnumerable<string> brands);
        Task<PaginationViewModel<ProductItem>> GetItemsFromCategorys(int skip, int take, IEnumerable<string> categorys);
        Task<PaginationViewModel<ProductItem>> GetItemsFromBrandsAndCategorys(int skip, int take, IEnumerable<string> brands, IEnumerable<string> categorys);

        Task<ProductItem> GetProduct(string productID);

        Task<Product> GetProductDetail(string productID);

        Task<IEnumerable<ProductItem>> GetRecomendedProducts(string userID);
    }
}
