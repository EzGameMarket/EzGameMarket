using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.ViewModels.Pagination;
using WebMVC.ViewModels.Products.Abstraction;
using WebMVC.ViewModels.Products.ProductSideBar.Abstraction;

namespace WebMVC.ViewModels.Products
{
    public class ProductsPageGridModel
    {
        public List<ProductItem> Products { get; set; }
        public ISideBar SideBar { get; set; }
        public PaginationInfo Pagination { get; set; }
    }
}
