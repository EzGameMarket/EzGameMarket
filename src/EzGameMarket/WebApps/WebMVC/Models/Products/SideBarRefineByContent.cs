using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models.Products.Abstraction;

namespace WebMVC.Models.Products
{
    public class SideBarRefineByContent : ISideBarRefineByContent
    {
        public string CategoryTitle { get; set; }
        public string CategoryData { get; set; }
        public List<IRefineByData> Data { get; set; }
    }
}
