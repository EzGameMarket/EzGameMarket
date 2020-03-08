using CatalogService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.ViewModels
{
    public class DiscountCreateViewModel
    {
        public Product Product { get; set; }
        public double DiscountPrice { get; set; }
    }
}
