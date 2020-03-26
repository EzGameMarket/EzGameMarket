using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models.Products.Abstraction;

namespace WebMVC.Models.Products
{
    public class RefineByData : IRefineByData
    {
        public string Data { get; set; }
        public string Title { get; set; }
        public bool Checked { get; set; }
        public bool Active { get; set; } = true;
    }
}
