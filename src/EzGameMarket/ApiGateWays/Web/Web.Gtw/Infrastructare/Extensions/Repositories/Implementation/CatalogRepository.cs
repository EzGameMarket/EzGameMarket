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
        public async Task<PaginationViewModel<CatalogItem>> GetItems(int skip, int take)
        {
            var url = API.Catalog.GetItems(_urls.Catalog);
            var parameters = new Dictionary<string, string>()
            {
                { "skip", $"{skip}" },
                { "take", $"{take}" }
            };

            url = QueryHelpers.AddQueryString(url, parameters);

            return await _client.GetDataWithGetAsync<PaginationViewModel<CatalogItem>>(url);
        }
    }
}
