using CatalogService.API.Models;

namespace CatalogService.API.ViewModels
{
    public class DiscountCreateViewModel
    {
        public Product Product { get; set; }
        public double DiscountPrice { get; set; }
    }
}