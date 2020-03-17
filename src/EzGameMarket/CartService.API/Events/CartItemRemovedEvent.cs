using CartService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartService.API.Events
{
    public class CartItemRemovedEvent
    {
        public CartItemRemovedEvent(Cart cart, CartItem removedItem)
        {
            Cart = cart;
            RemovedItem = removedItem;
        }

        public Cart Cart { get; set; }
        public CartItem RemovedItem { get; set; }
    }
}
