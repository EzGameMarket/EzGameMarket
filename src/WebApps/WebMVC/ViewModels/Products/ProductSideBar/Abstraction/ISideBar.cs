using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.ViewModels.Products.ProductSideBar.Filterize.Abstraction;

namespace WebMVC.ViewModels.Products.ProductSideBar.Abstraction
{
    public interface ISideBar
    {
        public string CategoryTitle { get; set; }
        public List<ISideBarCategoryContent> CategoryData { get; set; }
        public string RefineByTitle { get; set; }
        public List<ISideBarRefineByContent> RefineByData { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }
}
