using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.ViewModels.Products
{
    public class ProductViewModel
    {
        public IEnumerable<string> ImageUrls { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int DiscountedPrice { get; set; }
        public IEnumerable<string> Categorys { get; set; }
        public IEnumerable<string> Tags { get; set; }

        public string ShortDescription { get; set; }
        public string Description { get; set; }
    }
}
