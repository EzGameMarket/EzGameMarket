using CartService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartService.API.Events
{
    public class CartCheckoutEvent
    {
        public CartCheckoutEvent(Cart checkedoutCart)
        {
            CheckedoutCart = checkedoutCart;
        }

        public Cart CheckedoutCart { get; set; }


    }
}
