using CouponCode.API.Data;
using CouponCode.API.Exceptions.Discount;
using CouponCode.API.Models;
using CouponCode.API.Services.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponCode.API.Services.Repositories.Implementations
{
    public class DiscountRepository : IDiscountRepository
    {
        private CouponCodeDbContext _dbContext;

        public DiscountRepository(CouponCodeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(DiscountModel model)
        {
            if (model.PercentageDiscount > DiscountModel.MaximumPercentage)
            {
                throw new DiscountTooHighException() { Discount = model.PercentageDiscount };
            }


            if (model.ID != default)
            {
                var id = model.ID.GetValueOrDefault(-1);

                if (await AnyEntityWithID(id) == true)
                {
                    throw new DiscountAlreadyInDbWithIDException() { ID = id };
                }
            }

            if (await AnyEntityWithName(model.Name) == true)
            {
                throw new DiscountAlreadyInDbWithNameException() { Name = model.Name };
            }

            await _dbContext.Discounts.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }

        public Task<DiscountModel> GetByID(int id) => _dbContext.Discounts.FirstOrDefaultAsync(d=> d.ID == id);

        public Task<DiscountModel> GetByName(string name) => _dbContext.Discounts.FirstOrDefaultAsync(d => d.Name == name);

        public async Task Update(int id, DiscountModel model)
        {
            if (model.PercentageDiscount > DiscountModel.MaximumPercentage)
            {
                throw new DiscountTooHighException() { Discount = model.PercentageDiscount };
            }

            if (await AnyEntityWithID(id) == false)
            {
                throw new DiscountNotFoundException() { ID = id };
            }

            model.ID = id;

            _dbContext.Discounts.Update(model);
            await _dbContext.SaveChangesAsync();
        }

        public Task<bool> AnyEntityWithID(int id) => _dbContext.Discounts.AnyAsync(d => d.ID == id);

        public Task<bool> AnyEntityWithName(string name) => _dbContext.Discounts.AnyAsync(d => d.Name == name);
    }
}
