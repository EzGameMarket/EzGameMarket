using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Gtw.Models.ViewModels.Cart;
using Web.Gtw.Infrastructare.ServiceAccess;
using System.Net.Http;
using Shared.Extensions.HttpClientHandler;
using Web.Gtw.Services.Repositories.Abstractions;
using Web.Gtw.Infrastructare.ServiceAccess.Abstractions;

namespace Web.Gtw.Services.Repositories.Implementation
{
    public class CartRepository : ICartRepository
    {
        private IHttpHandlerUtil _client;
        private IServiceUrls _urls;

        public CartRepository(IHttpHandlerUtil client, IServiceUrls urls)
        {
            _client = client;
            _urls = urls;
        }

        public async Task Checkout(string userID, CheckoutViewModel model)
        {
            var sendModel = new SendCartUpdatesModel<CheckoutViewModel>()
            {
                Model = model,
                UserID = userID
            };

            await _client.SendDataWithPostAsync(sendModel, API.Cart.ChechoutCart(_urls.Cart));
        }

        public async Task Update(string userID, CartItemUpdateModel model)
        {
            var sendModel = new SendCartUpdatesModel<CartItemUpdateModel>()
            {
                Model = model,
                UserID = userID
            };

            await _client.SendDataWithPostAsync(sendModel, API.Cart.UpdateCart(_urls.Cart));
        }

        public Task<CartViewModel> GetCart(string userID) => _client.GetDataWithGetAsync<CartViewModel>(API.Cart.GetCart(_urls.Cart) + $"{userID}/");
    }
}
