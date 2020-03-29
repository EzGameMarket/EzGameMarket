using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.ViewModels.Carts;
using WebMVC.Services.Repositorys.Abstractions;

namespace WebMVC.Services.Repositorys.Implementation
{
    public class CartRepository : ICartRepository
    {
        public Task Checkout(CheckoutModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> GetCartAsync(string userID)
        {
            var data = new List<CartItemModel>()
            {
                new CartItemModel(){ ImageUrl = "test.png", Price = 500, ProductID = "minecraft", ProductName = "Minecraft Standard Edition", Quantity = 2 },
                new CartItemModel(){ ImageUrl = "test.png", Price = 50, ProductID = "bttflvdeled", ProductName = "Battlefield V Deluxe Edition", Quantity = 3 },
                new CartItemModel(){ ImageUrl = "test.png", Price = 1000, ProductID = "fifa2020", ProductName = "Fifa 20", Quantity = 1 }
            };
            var model = new Cart()
            {
                BuyerID = userID,
                CartItems = data
            };

            return model;
        }

        public Task Update(string userID,UpdateCartItemModel model)
        {
            throw new NotImplementedException();
        }
    }
}
