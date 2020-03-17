using CartService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartService.API.Events
{
    public class CartItemAddedEvent
    {
        public CartItemAddedEvent(Cart cart, CartItem addedItem)
        {
            Cart = cart;
            AddedItem = addedItem;
        }

        public Cart Cart { get; set; }
        public CartItem AddedItem { get; set; }
    }
}
