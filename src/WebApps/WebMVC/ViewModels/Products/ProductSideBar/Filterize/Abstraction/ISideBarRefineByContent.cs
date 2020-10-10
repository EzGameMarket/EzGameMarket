using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels.Products.ProductSideBar.Filterize.Abstraction
{
    public interface ISideBarRefineByContent
    {
        public string CategoryTitle { get; set; }
        public string CategoryData { get; set; }
        public List<IRefineByData> Data { get; set; }
    }
}
