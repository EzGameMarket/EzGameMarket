using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models.Carts
{
    public class AddCartItemModel
    {
        public string ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
