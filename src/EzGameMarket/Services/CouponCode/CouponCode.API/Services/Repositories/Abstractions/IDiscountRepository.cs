using CouponCode.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponCode.API.Services.Repositories.Abstractions
{
    public interface IDiscountRepository
    {
        Task<DiscountModel> GetByID(int id);
        Task<DiscountModel> GetByName(string name);

        Task<bool> AnyEntityWithID(int id);
        Task<bool> AnyEntityWithName(string name);

        Task Update(int id, DiscountModel model);
        Task Add(DiscountModel model);
    }
}
