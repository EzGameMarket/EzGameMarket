using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.ViewModels.Products.Abstraction;
using WebMVC.ViewModels.Products.ProductSideBar.Filterize.Abstraction;

namespace WebMVC.ViewModels.Products.ProductSideBar.Filterize
{
    public class RefineByData : IRefineByData
    {
        public string Data { get; set; }
        public string Title { get; set; }
        public bool Checked { get; set; }
        public bool Active { get; set; } = true;
    }
}
