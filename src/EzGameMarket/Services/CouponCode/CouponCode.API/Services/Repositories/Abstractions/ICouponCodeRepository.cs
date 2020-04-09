using CouponCode.API.Models;
using CouponCode.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponCode.API.Services.Repositories.Abstractions
{
    public interface ICouponCodeRepository
    {
        Task<CouponCodeModel> GetByID(int id);
        Task<CouponCodeModel> GetByCodeName(string code);

        Task<bool> Validate(ValidateCouponCodeViewModel model);

        Task Update(int id, CouponCodeModel model);
        Task Add(CouponCodeModel model);
        Task AddUsersToCoupon(string couponCode, IEnumerable<string> userIDs);
    }
}
