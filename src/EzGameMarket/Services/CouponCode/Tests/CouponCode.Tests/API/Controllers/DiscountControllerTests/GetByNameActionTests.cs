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
    public class GetByNameActionTests
    {
        [Fact]
        public async void GetByID_ShouldReturnSuccess()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new DiscountRepository(dbContext);

            var name = "KRISZW";

            //Act
            var controller = new DiscountController(repo);
            var actionResult = await controller.GetByName(name);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<DiscountModel>>(actionResult);
            var value = Assert.IsAssignableFrom<DiscountModel>(actionResult.Value);
            Assert.NotNull(value);
            Assert.Equal(name, value.Name);
        }

        [Fact]
        public async void GetByID_ShouldReturnBadRequestForIDMinus1()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new DiscountRepository(dbContext);

            var name = string.Empty;

            //Act
            var controller = new DiscountController(repo);
            var actionResult = await controller.GetByName(name);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void GetByID_ShouldReturnNotFoundForID100()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new DiscountRepository(dbContext);

            var name = "notexist";

            //Act
            var controller = new DiscountController(repo);
            var actionResult = await controller.GetByName(name);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
