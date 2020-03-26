using System.Collections.Generic;
using System.Linq;

namespace WebMVC.Models.Carts
{
    public class Cart
    {
        public string BuyerID { get; set; }
        public IEnumerable<CartItemModel> CartItems { get; set; }

        public int Total() => CartItems.Sum(c=> c.Price * c.Quantity);
    }
}