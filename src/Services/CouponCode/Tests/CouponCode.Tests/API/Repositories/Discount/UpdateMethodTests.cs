using CouponCode.API.Data;
using CouponCode.API.Exceptions.CouponCode;
using CouponCode.API.Exceptions.Discount;
using CouponCode.API.Models;
using CouponCode.API.Services.Repositories.Implementations;
using CouponCode.Tests.FakeImplementation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CouponCode.Tests.API.Repositories.Discount
{
    public class UpdateMethodTests
    {
        public DiscountModel CreateModel() => new DiscountModel()
        {
            Name = "KRISZW",
            PercentageDiscount = 1
        };

        [Fact]
        public async void Modify_ShouldReturnOneCouponCodeWithCODEKRISZW()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new DiscountRepository(dbContext);

            var id = 1;
            var model = CreateModel();
            model.ID = id;

            var expectedCouponCode = "KRISZW";
            var expectedDiscountPercentage = model.PercentageDiscount;

            //Act
            await repo.Update(id, model);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedCouponCode, actual.Name);
            Assert.Equal(expectedDiscountPercentage, actual.PercentageDiscount);
        }

        [Fact]
        public async void Modify_ShouldThrowDiscountNotFoundExceptionForID100()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new DiscountRepository(dbContext);

            var id = 100;
            var model = CreateModel();


            //Act
            var uploadTask = repo.Update(id, model);

            //Assert
            await Assert.ThrowsAsync<DiscountNotFoundException>(() => uploadTask);
        }

        [Fact]
        public async void Modify_ShouldThrowDiscountTooHighException()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new DiscountRepository(dbContext);

            var id = 1;
            var model = CreateModel();
            model.ID = 1;
            model.PercentageDiscount = 50;


            //Act
            var uploadTask = repo.Update(id, model);

            //Assert
            await Assert.ThrowsAsync<DiscountTooHighException>(() => uploadTask);
        }
    }
}
