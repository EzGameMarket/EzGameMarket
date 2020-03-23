using CartService.API.Models;
using CartService.API.Models.ViewModels;
using EventBus.Shared.Events;

namespace CartService.API.Events
{
    public class CartItemAddedIntegrationEvent : IntegrationEvent
    {
        public CartItemAddedIntegrationEvent(Cart cart, CartItemModifyModel addedItem)
        {
            Cart = cart;
            AddedItem = addedItem;
        }

        public Cart Cart { get; set; }
        public CartItemModifyModel AddedItem { get; set; }
    }
}