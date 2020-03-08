using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.ViewModels
{
    public class PriceUpdateViewModel
    {
        public string ProductID { get; set; }
        public double NewPrice { get; set; }
        public double NewDiscountedPrice { get; set; }
    }
}
