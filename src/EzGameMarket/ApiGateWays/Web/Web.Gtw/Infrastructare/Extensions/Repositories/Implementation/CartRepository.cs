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

        public async Task Checkout(CheckoutViewModel model)
        {
            var url = API.Cart.ChechoutCart(_urls.Cart);

            await _client.SendDataWithPostAsync(model,url);
        }

        public async Task<CartViewModel> GetCart(string userID)
        {
            var url = API.Cart.GetCart(_urls.Cart)+$"{userID}/";

            return await _client.GetDataWithGetAsync<CartViewModel>(url);
        }

        public async Task Update(CartItemUpdateModel model)
        {
            var url = API.Cart.UpdateCart(_urls.Cart);

            await _client.SendDataWithPostAsync(model, url);
        }
    }
}
