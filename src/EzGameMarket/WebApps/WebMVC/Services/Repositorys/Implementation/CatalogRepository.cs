using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Extensions.Settings;
using WebMVC.ViewModels.Products;
using WebMVC.Services.Repositorys.Abstractions;
using WebMVC.Utils;
using Microsoft.AspNetCore.WebUtilities;
using Shared.Extensions.HttpClientHandler;
using Shared.Extensions.Pagination;

namespace WebMVC.Services.Repositorys.Implementation
{
    public class CatalogRepository : ICatalogRepository
    {
        private IHttpHandlerUtil _client;
        private ILoadBalancerUrls _urls;

        private string BuildQueryUrl(string baseUrl, IDictionary<string, string> parameters) => QueryHelpers.AddQueryString(baseUrl, parameters);

        private Task<PaginationViewModel<ProductItem>> PerformQueryItems(IDictionary<string, string> parameters)
        {
            var url = GtwUrl.Catalog.GetItems(_urls.MainBalancer);
            url = BuildQueryUrl(url, parameters);

            return _client.GetDataWithGetAsync<PaginationViewModel<ProductItem>>(url);
        }

        private Task<PaginationViewModel<ProductItem>> PerformFilter(IDictionary<string, string> parameters)
        {
            var url = GtwUrl.Catalog.Filter.GetFilterUrl(_urls.MainBalancer);
            url = BuildQueryUrl(url, parameters);

            return _client.GetDataWithGetAsync<PaginationViewModel<ProductItem>>(url);
        }

        private string GetQueryStringFrom(IEnumerable<string> data)
        {
            var output = "";

            foreach (var item in data)
            {
                output += item + ",";
            }

            return output.TrimEnd(',');
        }

        public Task<PaginationViewModel<ProductItem>> GetItems(int skip, int take)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "skip", $"{skip}" },
                { "take", $"{take}" }
            };

            return PerformQueryItems(parameters);
        }

        public Task<PaginationViewModel<ProductItem>> GetItemsFromTagsAndCategories(int skip, int take, IEnumerable<string> tags, IEnumerable<string> categories)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "skip", $"{skip}" },
                { "take", $"{take}" },
                { $"{nameof(tags)}", $"{GetQueryStringFrom(tags)}" },
                { $"{nameof(categories)}", $"{GetQueryStringFrom(categories)}" }
            };

            return PerformFilter(parameters);
        }

        public Task<PaginationViewModel<ProductItem>> GetItemsFromCategories(int skip, int take, IEnumerable<string> categories)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "skip", $"{skip}" },
                { "take", $"{take}" },
                { $"{nameof(categories)}", $"{GetQueryStringFrom(categories)}" }
            };

            return PerformFilter(parameters);
        }

        public Task<PaginationViewModel<ProductItem>> GetItemsFromTags(int skip, int take, IEnumerable<string> tags)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "skip", $"{skip}" },
                { "take", $"{take}" },
                { $"{nameof(tags)}", $"{GetQueryStringFrom(tags)}" }
            };

            return PerformFilter(parameters);
        }

        public Task<PaginationViewModel<ProductItem>> FilterItems(int skip, int take, IEnumerable<string> tags = null, IEnumerable<string> categories = null)
        {
            if (tags != default && categories == default)
            {
                return GetItemsFromTags(skip, take, tags);
            }
            else if (tags == default && categories != default)
            {
                return GetItemsFromCategories(skip, take, categories);
            }
            else if(tags != default && categories != default)
            {
                return GetItemsFromTagsAndCategories(skip, take, tags, categories);
            }
            else
            {
                throw new ApplicationException("A tags és categories paraméterek is null volt");
            }
        }

        public Task<ProductItem> GetProduct(string productID)
        {
            var url = GtwUrl.Catalog.GetCatalogItem(_urls.MainBalancer) + $"{productID}";

            return _client.GetDataWithGetAsync<ProductItem>(url);
        }

        public Task<Product> GetProductDetail(string productID)
        {
            var url = GtwUrl.Catalog.GetProductInDetail(_urls.MainBalancer) + $"{productID}";

            return _client.GetDataWithGetAsync<Product>(url);
        }

        public Task<IEnumerable<ProductItem>> GetRecomendedProducts(string userID)
        {
            var url = GtwUrl.Catalog.GetRecomendedProducts(_urls.MainBalancer) + $"{userID}";

            return _client.GetDataWithGetAsync<IEnumerable<ProductItem>>(url);
        }
    }
}
