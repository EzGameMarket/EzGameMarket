using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.ViewModels.Products.Abstraction;

namespace WebMVC.ViewModels.Products
{
    public class DetailProduct : IContainsRecomendedProducts
    {
        public IEnumerable<ProductItem> RecomendedProducts { get; set; }

        public Product Product { get; set; }
    }
}
