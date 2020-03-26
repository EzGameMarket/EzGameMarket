using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models.Products.Abstraction
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
