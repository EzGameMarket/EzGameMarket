using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Gtw.Models.ViewModels.Catalog.SingleProduct
{
    public class Product
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int DiscountedPrice { get; set; }
        public int Price { get; set; }

        public CatalogImages Images { get; set; }

        public CloudGamingSupport CGSupport { get; set; }

        public Reviews ReviewsData { get; set; }

        public Tags TagsData { get; set; }
        public Categories CategoriesData { get; set; }

        public List<CatalogItem> RecommendedProducts { get; set; }
    }
}
