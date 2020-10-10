using System.Collections.Generic;
using System.Linq;

namespace WebMVC.ViewModels.Carts
{
    public class Cart
    {
        public int ID { get; set; }
        public string OwnerID { get; set; }
        public IEnumerable<CartItemModel> CartItems { get; set; }

        public int Total()
        {
            if (CartItems != default)
            {
                return CartItems.Sum(c => c.Price * c.Quantity);
            }
            else
            {
                return 0;
            }
        }
    }
}