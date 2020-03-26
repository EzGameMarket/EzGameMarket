using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models.Products.Abstraction;

namespace WebMVC.Models.Products
{
    public class Product : IProduct, IHasReview
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
        public SystemRequirement SystemRequirement { get; set; }
        public string HelpActivateKey { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}
