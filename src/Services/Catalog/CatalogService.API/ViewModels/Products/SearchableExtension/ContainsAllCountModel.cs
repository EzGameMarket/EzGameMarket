using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.ViewModels.Products.SearchableExtension
{
    public class ContainsAllCountModel<T> where T : class
    {
        public ContainsAllCountModel(long allCount, IEnumerable<T> data)
        {
            AllCount = allCount;
            Data = data;
        }

        public long AllCount { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
