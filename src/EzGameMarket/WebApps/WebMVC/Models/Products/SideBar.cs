using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models.Products.Abstraction;

namespace WebMVC.Models.Products
{
    public class SideBar : ISideBar
    {
        public string CategoryTitle { get; set; }
        public List<ISideBarCategoryContent> CategoryData { get; set; }
        public string RefineByTitle { get; set; }
        public List<ISideBarRefineByContent> RefineByData { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }
}
