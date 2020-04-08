using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Gtw.Models.ViewModels;
using Web.Gtw.Models.ViewModels.Cart;

namespace Web.Gtw.Services.Repositories.Abstractions
{
    public interface ICartRepository
    {
        Task Update(string userID,CartItemUpdateModel model);

        Task Checkout(string userID, CheckoutViewModel model);

        Task<CartViewModel> GetOwnCart();
        Task<CartViewModel> GetCart(string userID);
    }
}
