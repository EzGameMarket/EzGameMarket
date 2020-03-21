using CartService.API.Models;
using CartService.API.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartService.API.Services
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByCustomerIDAsync(string id);
        Task AddItemToCartAsync(string id,CartItemModifyModel item);
        Task RemoveItemFromCartAsync(string id,CartItemModifyModel item);
        Task<Cart> CreateCartAsync(string id);

        Task Checkout(string id, CheckoutModel model);
    }
}
