using Microsoft.AspNetCore.WebUtilities;
using Shared.Extensions.HttpClientHandler;
using Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Gtw.Services.Repositories.Abstractions;
using Web.Gtw.Infrastructare.ServiceAccess;
using Web.Gtw.Models;
using Web.Gtw.Models.ViewModels.Catalog;
using Web.Gtw.Infrastructare.ServiceAccess.Abstractions;
using Web.Gtw.Models.ViewModels.Catalog.SingleProduct;

namespace Web.Gtw.Services.Repositories.Implementation
{
    public class CatalogRepository : ICatalogRepository
    {
        private IHttpHandlerUtil _client;
        private IServiceUrls _urls;

        public CatalogRepository(IHttpHandlerUtil client, IServiceUrls urls)
        {
            _client = client;
            _urls = urls;
        }

        public Task<PaginationViewModel<CatalogItem>> Filter(int skip, int take, IEnumerable<string> brands, IEnumerable<string> categories, IEnumerable<string> tags)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetDetail(string productID)
        {
            var catalogUrl = API.Catalog.GetProductDetail(_urls.Catalog) + productID;
            var catalogImagesUrl = API.CatalogImages.GetImagesForItem(_urls.Catalog) + productID;
            var categoriesUrl = "";
            var filtersUrl = "";

            var catalogTask = _client.GetDataWithGetAsync<Product>(catalogUrl);
            var imagesTask = _client.GetDataWithGetAsync<Product>(catalogImagesUrl);
            var filtersTask = _client.GetDataWithGetAsync<Product>(filtersUrl);
            var categoriesTask = _client.GetDataWithGetAsync<Product>(categoriesUrl);

            var accesses = new Task[]
            {
                catalogTask,
                imagesTask,
                filtersTask,
                categoriesTask,
            };

            await Task.WhenAll(accesses);

            return default;
        }

        public Task<PaginationViewModel<CatalogItem>> GetItems(int skip, int take)
        {
            var url = API.Catalog.GetItems(_urls.Catalog);
            var parameters = new Dictionary<string, string>()
            {
                { "skip", $"{skip}" },
                { "take", $"{take}" }
            };

            url = QueryHelpers.AddQueryString(url, parameters);

            return _client.GetDataWithGetAsync<PaginationViewModel<CatalogItem>>(url);
        }

        public Task<IEnumerable<CatalogItem>> GetRecomended(string userID)
        {
            throw new NotImplementedException();
        }
    }
}
