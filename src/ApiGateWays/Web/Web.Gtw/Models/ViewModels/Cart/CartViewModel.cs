using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Gtw.Models.ViewModels.Cart
{
    public class CartViewModel
    {
        public int ID { get; set; }

        public string OwnerID { get; set; }

        public List<CartItemViewModel> Items { get; set; }
    }
}
