using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.ViewModels.Products.Abstraction;
using WebMVC.ViewModels.Products.ProductSideBar.Filterize.Abstraction;

namespace WebMVC.ViewModels.Products.ProductSideBar.Filterize
{
    public class SideBarRefineByContent : ISideBarRefineByContent
    {
        public string CategoryTitle { get; set; }
        public string CategoryData { get; set; }
        public List<IRefineByData> Data { get; set; }
    }
}
