using CartService.API.Data;
using CartService.API.Events;
using CartService.API.Models;
using CartService.API.Models.ViewModels;
using EventBus.Shared.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CartService.API.Services
{
    public class CartRepository : ICartRepository
    {
        private CartDbContext _dbContext;
        private IEventBusRepository _eventBus;

        public CartRepository(CartDbContext dbContext,
                              IEventBusRepository eventBus)
        {
            _dbContext = dbContext;
            _eventBus = eventBus;
        }

        public async Task<Cart> GetCartByCustomerIDAsync(string id)
        {
            var cart = await _dbContext.Cart.FirstOrDefaultAsync(c => c.OwnerID == id);

            if (cart == default)
            {
                return await CreateCartAsync(id);
            }

            return cart;
        }

        public async Task UpdateCartAsync(string id, CartItemModifyModel item)
        {
            var cart = await GetCartByCustomerIDAsync(id);

            cart.Update(item);

            _eventBus.Publish(new CartItemAddedIntegrationEvent(cart, item));
        }

        public async Task<Cart> CreateCartAsync(string id)
        {
            var cart = new Cart() { OwnerID = id };

            await _dbContext.Cart.AddAsync(cart);
            await _dbContext.SaveChangesAsync();

            return cart;
        }

        public async Task Checkout(string id, CheckoutModel model)
        {
            var cart = await GetCartByCustomerIDAsync(id);

            _eventBus.Publish(new CartCheckoutIntegrationEvent(cart, model));
        }
    }
}