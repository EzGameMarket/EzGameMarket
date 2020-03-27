using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Gtw.Infrastructare.ServiceAccess
{
    public static class API
    {
        public const string apiVersion = "v1";

        public static class Cart
        {
            public static string UpdateCart(string baseUrl) => $"{GetBase(baseUrl)}/cart/update";
            public static string ChechoutCart(string baseUrl) => $"{GetBase(baseUrl)}/cart/checkout";
            public static string GetCart(string baseUrl) => $"{GetBase(baseUrl)}/cart/";


        }
        public static class Catalog
        {

        }

        private static string GetBase(string baseUrl) => $"{baseUrl}/api/{apiVersion}";

    }
}
