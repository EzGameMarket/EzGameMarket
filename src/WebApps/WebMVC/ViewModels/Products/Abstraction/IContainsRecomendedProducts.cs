using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels.Products.Abstraction
{
    public interface IContainsRecomendedProducts
    {
        IEnumerable<ProductItem> RecomendedProducts { get; set; }
    }
}
