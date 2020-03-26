using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models.Pagination;
using WebMVC.Models.Products.Abstraction;

namespace WebMVC.Models.Products
{
    public class ProductsPageGridModel
    {
        public List<ProductItem> Products { get; set; }
        public ISideBar SideBar { get; set; }
        public PaginationInfo Pagination { get; set; }
    }
}
