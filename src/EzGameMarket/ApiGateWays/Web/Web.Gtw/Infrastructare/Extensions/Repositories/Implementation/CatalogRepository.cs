using Microsoft.AspNetCore.WebUtilities;
using Shared.Extensions.HttpClientHandler;
using Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Gtw.Infrastructare.Extensions.Repositories.Abstractions;
using Web.Gtw.Infrastructare.ServiceAccess;
using Web.Gtw.Models;
using Web.Gtw.Models.ViewModels.Catalog;


namespace Web.Gtw.Infrastructare.Extensions.Repositories.Implementation
{
    public class CatalogRepository : ICatalogRepository
    {
        private IHttpHandlerUtil _client;
        private ServiceUrls _urls;

        public CatalogRepository(IHttpHandlerUtil client, ServiceUrls urls)
        {
            _client = client;
            _urls = urls;
        }

        public Task<PaginationViewModel<CatalogItem>> Filter(int skip, int take, IEnumerable<string> brands, IEnumerable<string> categories, IEnumerable<string> tags)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetDetail(string productID)
        {
            var url = API.Catalog.GetProductDetail(_urls.Catalog) + productID;

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
