using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.ViewModels.Carts;

namespace WebMVC.Services.Repositorys.Abstractions
{
    public interface ICartRepository
    {
        Task<Cart> GetCartAsync(string userID);
        Task Update(string userID,UpdateCartItemModel model);
        Task Checkout(string userID,CheckoutModel model);
    }
}
