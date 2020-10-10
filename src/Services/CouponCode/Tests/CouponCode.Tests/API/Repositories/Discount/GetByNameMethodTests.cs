using CouponCode.API.Data;
using CouponCode.API.Services.Repositories.Implementations;
using CouponCode.Tests.FakeImplementation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CouponCode.Tests.API.Repositories.Discount
{
    public class GetByNameMethodTests
    {
        [Fact]
        public async void GetByName_ShouldReturnCouponCodeForNameKRISZW()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new DiscountRepository(dbContext);

            var name = "KRISZW";

            //Act
            var item = await repo.GetByName(name);

            //Assert
            Assert.NotNull(item);
            Assert.Equal(name, item.Name);
        }

        [Fact]
        public async void GetByName_ShouldReturnNullForEmptyString()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new DiscountRepository(dbContext);

            var name = string.Empty;

            //Act
            var item = await repo.GetByName(name);

            //Assert
            Assert.Null(item);
        }

        [Fact]
        public async void GetByName_ShouldReturnNullForTestDc()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new DiscountRepository(dbContext);

            var name = "TestDc";

            //Act
            var item = await repo.GetByName(name);

            //Assert
            Assert.Null(item);
        }
    }
}
