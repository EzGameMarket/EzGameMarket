using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.ViewModels.Products
{
    public class CatalogItem
    {
        public CatalogItem(string imageUrl, string iD, string name, int price, int discountedPrice, string category, string description, string shortDescription)
        {
            ImageUrl = imageUrl;
            ID = iD;
            Name = name;
            Price = price;
            DiscountedPrice = discountedPrice;
            Category = category;
            Description = description;
            ShortDescription = shortDescription;
        }

        public string ImageUrl { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int DiscountedPrice { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
    }
}
