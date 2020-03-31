using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.ViewModels.Carts;
using WebMVC.Services.Repositorys.Abstractions;
using WebMVC.Utils;
using Shared.Extensions.HttpClientHandler;
using WebMVC.Extensions.Settings;

namespace WebMVC.Services.Repositorys.Implementation
{
    public class CartRepository : ICartRepository
    {
        private IHttpHandlerUtil _client;
        private ILoadBalancerUrls _urls;

        public CartRepository(IHttpHandlerUtil client, ILoadBalancerUrls urls)
        {
            _client = client;
            _urls = urls;
        }

        public async Task<Cart> GetCartAsync(string userID)
        {
            var url = GtwUrl.Cart.GetCart(_urls.MainBalancer)+userID;

            var cart = await _client.GetDataWithGetAsync<Cart>(url);

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

        public async Task Update(string userID,UpdateCartItemModel model)
        {
            var url = GtwUrl.Cart.UpdateCart(_urls.MainBalancer);
            var sendModel = new SendCartToUpdateViewModel<UpdateCartItemModel>() { UserName = userID, Data = model };

            await _client.SendDataWithPostAsync(sendModel,url);
        }

        public async Task Checkout(string userID, CheckoutModel model)
        {
            var url = GtwUrl.Cart.CheckoutCart(_urls.MainBalancer);
            var sendModel = new SendCartToUpdateViewModel<CheckoutModel>() { UserName = userID, Data = model };

            await _client.SendDataWithPostAsync(sendModel, url);
        }
    }
}
