using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models.Carts
{
    public class CheckoutModel
    {
        public List<Address> CostumerAddress { get; set; }
        public Cart BuyerCart { get; set; }
        public Costumer Costumer { get; set; }
    }
}
