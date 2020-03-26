using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models.Products.Abstraction
{
    public interface ISideBarCategoryContent
    {
        public string Title { get; set; }
        public string Data { get; set; }
        public bool Active { get; set; }
    }
}
