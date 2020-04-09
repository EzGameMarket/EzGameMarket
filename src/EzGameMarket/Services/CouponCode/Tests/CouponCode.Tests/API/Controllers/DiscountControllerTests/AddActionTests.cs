using CouponCode.API.Controllers;
using CouponCode.API.Data;
using CouponCode.API.Models;
using CouponCode.API.Services.Repositories.Implementations;
using CouponCode.Tests.FakeImplementation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CouponCode.Tests.API.Controllers.DiscountControllerTests
{
    public class AddActionTests
    {
        private DiscountModel CreateModel() => new DiscountModel()
        {
            Name = "AddActionTests",
            PercentageDiscount = 10
        };

        [Fact]
        public async void Add_ShouldReturnSuccess()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new DiscountRepository(dbContext);

            var model = CreateModel();

            //Act
            var controller = new DiscountController(repo);
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async void Add_ShouldReturnBadRequestForEmptyName()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new DiscountRepository(dbContext);

            var model = CreateModel();
            model.Name = string.Empty;

            //Act
            var controller = new DiscountController(repo);
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Add_ShouldReturnBadRequestForIDMinus1()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new DiscountRepository(dbContext);

            var model = CreateModel();
            model.ID = -1;

            //Act
            var controller = new DiscountController(repo);
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Add_ShouldReturnBadRequestForInvalidModelState()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new DiscountRepository(dbContext);

            var model = CreateModel();
            model.Name = default;

            //Act
            var controller = new DiscountController(repo);
            controller.ModelState.AddModelError("Name","A Name nem lehet null");
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Add_ShouldConflictForID1WithMessageInIt()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new DiscountRepository(dbContext);

            var model = CreateModel();
            model.ID = 1;

            var expectedMessage = "ID";

            //Act
            var controller = new DiscountController(repo);
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            var actual = Assert.IsAssignableFrom<ConflictObjectResult>(actionResult);
            Assert.Equal(expectedMessage, actual.Value.ToString());
        }

        [Fact]
        public async void Add_ShouldConflictForNameKRISZWWithMessageInIt()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new DiscountRepository(dbContext);

            var model = CreateModel();
            model.Name = "KRISZW";

            var expectedMessage = "Name";

            //Act
            var controller = new DiscountController(repo);
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            var actual = Assert.IsAssignableFrom<ConflictObjectResult>(actionResult);
            Assert.Equal(expectedMessage, actual.Value.ToString());
        }

        [Fact]
        public async void Add_ShouldReturnBadRequestObjectResultWithMessageInItForTooHighDiscount()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new DiscountRepository(dbContext);

            var model = CreateModel();
            model.PercentageDiscount = 50;

            var expectedMessage = "Discount is too high";

            //Act
            var controller = new DiscountController(repo);
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            var actual = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);
            Assert.Equal(expectedMessage, actual.Value.ToString());
        }
    }
}
