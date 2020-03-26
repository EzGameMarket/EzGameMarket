using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models.Products.Abstraction;

namespace WebMVC.Models.Products
{
    public class SideBarCategoryContent : ISideBarCategoryContent
    {
        public string Title { get; set; }
        public string Data { get; set; }
        public bool Active { get; set; } = true;
    }
}
