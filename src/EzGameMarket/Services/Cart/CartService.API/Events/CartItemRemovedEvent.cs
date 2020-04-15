using CartService.API.Models;
using CartService.API.Models.ViewModels;
using EventBus.Shared.Events;

namespace CartService.API.Events
{
    public class CartItemRemovedIntegrationEvent : IntegrationEvent
    {
        public CartItemRemovedIntegrationEvent(Cart cart, CartItemModifyModel removedItem)
        {
            Cart = cart;
            RemovedItem = removedItem;
        }

        public Cart Cart { get; set; }
        public CartItemModifyModel RemovedItem { get; set; }
    }
}