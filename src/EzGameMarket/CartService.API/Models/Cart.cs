using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartService.API.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public int OwnerID { get; set; }

        public List<CartItem> Items { get; set; }
    }
}
