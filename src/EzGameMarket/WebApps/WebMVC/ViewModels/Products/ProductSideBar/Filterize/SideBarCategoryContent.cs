using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.ViewModels.Products.Abstraction;
using WebMVC.ViewModels.Products.ProductSideBar.Filterize.Abstraction;

namespace WebMVC.ViewModels.Products.ProductSideBar.Filterize
{
    public class SideBarCategoryContent : ISideBarCategoryContent
    {
        public string Title { get; set; }
        public string Data { get; set; }
        public bool Active { get; set; } = true;
    }
}
