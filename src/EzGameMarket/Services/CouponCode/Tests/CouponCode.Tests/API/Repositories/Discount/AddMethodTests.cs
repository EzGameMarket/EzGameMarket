using CouponCode.API.Data;
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
    public class AddMethodTests
    {
        public DiscountModel CreateModel() => new DiscountModel()
        {
            Name = "AddTestCode",
            PercentageDiscount = 15
        };

        [Fact]
        public async void Add_ShouldReturnOneCouponCodeWithCODEKRISZW()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new DiscountRepository(dbContext);

            var model = CreateModel();

            //Act
            await repo.Add(model);
            var actual = await repo.GetByName(model.Name);

            //Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public async void Add_ShouldThrowDiscountConflictForID1()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new DiscountRepository(dbContext);

            var id = 1;
            var model = CreateModel();
            model.ID = id;

            //Act
            var uploadTask = repo.Add(model);

            //Assert
            await Assert.ThrowsAsync<DiscountAlreadyInDbWithIDException>(() => uploadTask);
        }

        [Fact]
        public async void Add_ShouldThrowDiscountConflictForNameKRISZW()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new DiscountRepository(dbContext);

            
            var model = CreateModel();
            model.Name = "KRISZW";

            //Act
            var uploadTask = repo.Add(model);

            //Assert
            await Assert.ThrowsAsync<DiscountAlreadyInDbWithNameException>(() => uploadTask);
        }

        [Fact]
        public async void Add_ShouldThrowDiscountTooHighException()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new DiscountRepository(dbContext);

            var model = CreateModel();
            model.PercentageDiscount = 50;


            //Act
            var uploadTask = repo.Add(model);

            //Assert
            await Assert.ThrowsAsync<DiscountTooHighException>(() => uploadTask);
        }
    }
}
