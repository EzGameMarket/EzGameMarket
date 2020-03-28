using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Gtw.Models.ViewModels;
using Web.Gtw.Models.ViewModels.Cart;

namespace Web.Gtw.Infrastructare.Extensions.Repositories.Abstractions
{
    public interface ICartRepository
    {
        Task Update(CartItemUpdateModel model);

        Task Checkout(CheckoutViewModel model);

        Task<CartViewModel> GetCart(string userID);
    }
}
