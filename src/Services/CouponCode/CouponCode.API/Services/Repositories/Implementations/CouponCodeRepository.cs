using CouponCode.API.Data;
using CouponCode.API.Exceptions;
using CouponCode.API.Exceptions.CouponCode;
using CouponCode.API.Exceptions.CouponCode.CouponCodeValidation;
using CouponCode.API.Models;
using CouponCode.API.Services.Repositories.Abstractions;
using CouponCode.API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponCode.API.Services.Repositories.Implementations
{
    public class CouponCodeRepository : ICouponCodeRepository
    {
        private CouponCodeDbContext _dbContext;

        public CouponCodeRepository(CouponCodeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(CouponCodeModel model)
        {
            var coupon = await GetByCodeName(model.Code);

            if (coupon != default)
            {
                throw new CouponCodeAlreadyUploadedException() { CouponCode = model.Code };
            }

            var couponWithID = await GetByID(model.ID.GetValueOrDefault(-1));

            if (couponWithID != default)
            {
                throw new CouponWithIDAlreadyUploadedException() { ID = model.ID.GetValueOrDefault(-1)};
            }

            if (model.Users != default && model.Users.Any() && model.IsLimitedForUsers == false)
            {
                model.IsLimitedForUsers = true;
            }

            await _dbContext.CouponCodes.AddAsync(model);

            await _dbContext.SaveChangesAsync();
        }

        public async Task AddUsersToCoupon(string couponCode, IEnumerable<string> userIDs)
        {
            var coupon = await GetByCodeName(couponCode);

            if (coupon == default)
            {
                throw new CouponCodeNotFoundException() { CouponCode = couponCode };
            }

            var users = userIDs.Select(u => new EligibleUserModel() { UserID = u });

            if (coupon.IsLimitedForUsers == false)
            {
                coupon.IsLimitedForUsers = true;
            }

            coupon.Users.AddRange(users);

            await _dbContext.SaveChangesAsync();
        }

        public Task<CouponCodeModel> GetByCodeName(string code) => _dbContext.CouponCodes.Include(c => c.Users).Include(c => c.Discount).FirstOrDefaultAsync(cc => cc.Code == code);

        public Task<CouponCodeModel> GetByID(int id) => _dbContext.CouponCodes.Include(c=> c.Users).Include(c=> c.Discount).FirstOrDefaultAsync(cc=> cc.ID == id);

        public async Task Update(int id, CouponCodeModel model)
        {
            var coupon = await GetByID(id);

            if (coupon == default)
            {
                throw new CouponCodeWithIDNotFoundException() { ID = id };
            }

            model.ID = id;

            _dbContext.Entry(coupon).CurrentValues.SetValues(model);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Validate(ValidateCouponCodeViewModel model)
        {
            if (string.IsNullOrEmpty(model.CouponCode) == false && string.IsNullOrEmpty(model.UserID) == false)
            {
                var coupon = await GetByCodeName(model.CouponCode);

                if (coupon != default)
                {
                    if (coupon.IsLimitedForUsers && coupon.Users.Any(u => u.UserID == model.UserID) == false)
                    {
                        throw new UserNotEligibleForCouponCodeException() { CouponCode = model.CouponCode, UserID = model.UserID };
                    }

                    if (coupon.EndDate < DateTime.Now)
                    {
                        throw new CouponCodeOutdatedException() { CouponCode = model.CouponCode, ValidToDate = coupon.EndDate.GetValueOrDefault() } ;
                    }

                    return true;
                }
                else
                {
                    throw new CouponCodeNotFoundException() { CouponCode = model.CouponCode } ;
                }
            }

            return false;
        }
    }
}
