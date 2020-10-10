using CartService.API.Models;
using CartService.API.Models.ViewModels;
using EventBus.Shared.Events;

namespace CartService.API.Events
{
    public class CartCheckoutIntegrationEvent : IntegrationEvent
    {
        public CartCheckoutIntegrationEvent(Cart checkedoutCart, CheckoutModel model)
        {
            CheckedoutCart = checkedoutCart;
            CheckoutModel = model;
        }

        public Cart CheckedoutCart { get; set; }

        public CheckoutModel CheckoutModel { get; set; }
    }
}