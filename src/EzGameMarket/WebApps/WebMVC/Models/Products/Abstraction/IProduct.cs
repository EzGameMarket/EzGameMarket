using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models.Products.Abstraction
{
    public interface IProduct
    {
        string ID { get; set; }
        string Name { get; set; }
        int Price { get; set; }
        int DiscountedPrice { get; set; }
    }
}
