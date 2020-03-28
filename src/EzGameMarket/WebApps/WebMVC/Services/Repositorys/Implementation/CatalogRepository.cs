using Shared.Extensions.HttpClientHandler;
using Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models.Pagination;
using WebMVC.Models.Products;
using WebMVC.Services.Repositorys.Abstractions;

namespace WebMVC.Services.Repositorys.Implementation
{
    public class CatalogRepository : ICatalogRepository
    {
        private IHttpHandlerUtil _client;

        public Task<PaginationViewModel<ProductItem>> GetItems(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Task<PaginationViewModel<ProductItem>> GetItemsFromBrands(int skip, int take, IEnumerable<string> brands)
        {
            throw new NotImplementedException();
        }

        public Task<PaginationViewModel<ProductItem>> GetItemsFromBrandsAndCategorys(int skip, int take, IEnumerable<string> brands, IEnumerable<string> categorys)
        {
            throw new NotImplementedException();
        }

        public Task<PaginationViewModel<ProductItem>> GetItemsFromCategorys(int skip, int take, IEnumerable<string> categorys)
        {
            throw new NotImplementedException();
        }

        public Task<ProductItem> GetProduct(string productID)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductDetail(string productID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductItem>> GetRecomendedProducts(string userID)
        {
            throw new NotImplementedException();
        }
    }
}
