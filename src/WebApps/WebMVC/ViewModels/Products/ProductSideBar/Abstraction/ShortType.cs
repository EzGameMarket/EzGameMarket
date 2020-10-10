using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels.Products.ProductSideBar.Abstraction
{
    public enum ShortType
    {
        AToZ = 0,
        ZToA = 1,
        LowToHigh = 2,
        HighToLow = 3,
        Newest = 4,
        Oldest = 5,
        MostPopular = 6,
        LeastPopular = 7
    }
}
