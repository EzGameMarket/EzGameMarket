using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Gtw.Models.ViewModels;

namespace Web.Gtw.Services
{
    public interface ICartRepository
    {
        Task<bool> Update(CartItemUpdateModel model);
    }
}
