using CouponCode.API.Data;
using CouponCode.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CouponCode.Tests.FakeImplementation
{
    public static class FakeDbContextCreator
    {


        //System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        public static DbContextOptions CreateDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<CouponCodeDbContext>()
                .UseInMemoryDatabase(databaseName: $"in-memory-couponcode-test-{dbName}").EnableSensitiveDataLogging()
                .Options;
        }

        public static async Task InitDbContext(DbContextOptions options)
        {
            using (var dbContext = new CouponCodeDbContext(options))
            {
                await dbContext.AddRangeAsync(CreateDiscounts());
                await dbContext.AddRangeAsync(CreateCoponCodes());
                await dbContext.SaveChangesAsync();
            }
        }

        public static List<CouponCodeModel> CreateCoponCodes() => new List<CouponCodeModel>()
        {
            new CouponCodeModel()
            {
                Code = "BLCKFRDY",
                DiscountID = 3,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                IsLimitedForUsers = false
            },
            new CouponCodeModel()
            {
                Code = "BLCKFRDY2021",
                DiscountID = 3,
                StartDate = DateTime.Now.AddYears(1),
                EndDate = DateTime.Now.AddYears(1).AddDays(2),
                IsLimitedForUsers = false
            },
            new CouponCodeModel()
            {
                Code = "KRISZW",
                DiscountID = 1,
                StartDate = DateTime.Now.AddDays(-30),
                EndDate = DateTime.Now.AddDays(30),
                IsLimitedForUsers = true,
                Users = new List<EligibleUserModel>()
                {
                    new EligibleUserModel()
                    {
                        UserID = "kriszw"
                    }
                }
            },
            new CouponCodeModel()
            {
                Code = "HÚSVÉT",
                DiscountID = 2,
                StartDate = DateTime.Now.AddDays(15),
                EndDate = DateTime.Now.AddDays(20),
                IsLimitedForUsers = false
            },
            new CouponCodeModel()
            {
                Code = "HÚSVÉT2021",
                DiscountID = 2,
                StartDate = DateTime.Now.AddYears(1).AddDays(10),
                EndDate = DateTime.Now.AddYears(1).AddDays(15),
                IsLimitedForUsers = false
            },
            new CouponCodeModel()
            {
                Code = "OUTDATED",
                DiscountID = 2,
                StartDate = DateTime.Now.AddYears(-1),
                EndDate = DateTime.Now.AddYears(-1).AddDays(1),
                IsLimitedForUsers = false
            },
        };

        public static List<DiscountModel> CreateDiscounts() => new List<DiscountModel>() 
        {
            new DiscountModel()
            {
                ID = default,
                Name = "KRISZW",
                PercentageDiscount = 5
            },
            new DiscountModel()
            {
                ID = default,
                Name = "Húsvét",
                PercentageDiscount = 10
            },
            new DiscountModel()
            {
                ID = default,
                Name = "BlackFriday",
                PercentageDiscount = 15
            },
        };
    }
}
