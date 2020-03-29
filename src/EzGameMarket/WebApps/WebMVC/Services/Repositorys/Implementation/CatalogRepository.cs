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

        public Task<PaginationViewModel<ProductItem>> GetItemsFromBrands(int skip, int take, IEnumerable<string> brands)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "skip", $"{skip}" },
                { "take", $"{take}" },
                { $"{nameof(brands)}", $"{GetQueryStringFrom(brands)}" }
            };

            return PerformFilter(parameters);
        }

        public Task<PaginationViewModel<ProductItem>> GetItemsFromBrandsAndCategorys(int skip, int take, IEnumerable<string> brands, IEnumerable<string> categorys)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "skip", $"{skip}" },
                { "take", $"{take}" },
                { $"{nameof(brands)}", $"{GetQueryStringFrom(brands)}" },
                { $"{nameof(categorys)}", $"{GetQueryStringFrom(categorys)}" }
            };

            return PerformFilter(parameters);
        }

        public Task<PaginationViewModel<ProductItem>> GetItemsFromBrandsAndCategorysAndTags(int skip, int take, IEnumerable<string> brands, IEnumerable<string> categorys, IEnumerable<string> tags)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "skip", $"{skip}" },
                { "take", $"{take}" },
                { $"{nameof(brands)}", $"{GetQueryStringFrom(brands)}" },
                { $"{nameof(categorys)}", $"{GetQueryStringFrom(categorys)}" },
                { $"{nameof(tags)}", $"{GetQueryStringFrom(tags)}" },
            };

            return PerformFilter(parameters);
        }

        public Task<PaginationViewModel<ProductItem>> GetItemsFromBrandsAndTags(int skip, int take, IEnumerable<string> brands, IEnumerable<string> tags)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "skip", $"{skip}" },
                { "take", $"{take}" },
                { $"{nameof(brands)}", $"{GetQueryStringFrom(brands)}" },
                { $"{nameof(tags)}", $"{GetQueryStringFrom(tags)}" },
            };

            return PerformFilter(parameters);
        }

        public Task<PaginationViewModel<ProductItem>> GetItemsFromCategorys(int skip, int take, IEnumerable<string> categorys)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "skip", $"{skip}" },
                { "take", $"{take}" },
                { $"{nameof(categorys)}", $"{GetQueryStringFrom(categorys)}" }
            };

            return PerformFilter(parameters);
        }

        public Task<PaginationViewModel<ProductItem>> GetItemsFromCategorysAndTags(int skip, int take, IEnumerable<string> categorys, IEnumerable<string> tags)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "skip", $"{skip}" },
                { "take", $"{take}" },
                { $"{nameof(categorys)}", $"{GetQueryStringFrom(categorys)}" },
                { $"{nameof(tags)}", $"{GetQueryStringFrom(tags)}" }
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
