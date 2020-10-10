using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Utils
{
    public static class GtwUrl
    {
        private static string _version = "/";
        //private static string _version = "/v1/";

        public static class Catalog
        {
            public static string GetItems(string baseUrl) => BuildBaseUrlWithVersion(baseUrl) + "catalog/items/";
            public static string GetCatalogItem(string baseUrl) => BuildBaseUrlWithVersion(baseUrl) + "catalog/item/";
            public static string GetProductInDetail(string baseUrl) => BuildBaseUrlWithVersion(baseUrl) + "catalog/product/";
            public static string GetRecomendedProducts(string baseUrl) => BuildBaseUrlWithVersion(baseUrl) + "catalog/recommended/";

            public static class Filter
            {
                public static string GetFilterUrl(string baseUrl) => BuildBaseUrlWithVersion(baseUrl) + "catalog/filter/";
            }
            public static class Search
            {
                public static string GetSearch(string baseUrl) => BuildBaseUrlWithVersion(baseUrl) + "search/";
            }
        }
        public static class Cart
        {
            public static string GetCart(string baseUrl) => BuildBaseUrlWithVersion(baseUrl) + "cart/";
            public static string UpdateCart(string baseUrl) => BuildBaseUrlWithVersion(baseUrl) + "cart/update";
            public static string CheckoutCart(string baseUrl) => BuildBaseUrlWithVersion(baseUrl) + "cart/checkout";
        }

        private static string BuildBaseUrlWithVersion(string baseUrl) => baseUrl +"/api"+ _version;
    }
}
