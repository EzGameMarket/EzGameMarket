using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Gtw.Models.ViewModels.Cart;
using Web.Gtw.Infrastructare.ServiceAccess;
using System.Net.Http;
using Shared.Extensions.HttpClientHandler;
using Web.Gtw.Infrastructare.Extensions.Repositories.Abstractions;

namespace Web.Gtw.Infrastructare.Extensions.Repositories.Implementation
{
    public class CartRepository : ICartRepository
    {
        private IHttpHandlerUtil _client;
        private ServiceUrls _urls;

        public CartRepository(IHttpHandlerUtil client, ServiceUrls urls)
        {
            _client = client;
            _urls = urls;
        }

        public Task Checkout(CheckoutViewModel model) => _client.SendDataWithPostAsync(model, API.Cart.ChechoutCart(_urls.Cart));

        public Task<CartViewModel> GetCart(string userID) => _client.GetDataWithGetAsync<CartViewModel>(API.Cart.GetCart(_urls.Cart) + $"{userID}/");

        public Task Update(CartItemUpdateModel model) => _client.SendDataWithPostAsync(model, API.Cart.UpdateCart(_urls.Cart));
    }
}
