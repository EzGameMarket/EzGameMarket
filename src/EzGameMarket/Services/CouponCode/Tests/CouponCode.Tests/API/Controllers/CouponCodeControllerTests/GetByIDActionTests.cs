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

namespace CouponCode.Tests.API.Controllers.CouponCodeControllerTests
{
    public class GetByIDActionTests
    {
        [Fact]
        public async void GetByID_ShouldReturnSuccess()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var id = 1;

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.GetByID(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<CouponCodeModel>>(actionResult);
            var value = Assert.IsAssignableFrom<CouponCodeModel>(actionResult.Value);
            Assert.NotNull(value);
            Assert.Equal(id, value.ID);
        }

        [Fact]
        public async void GetByID_ShouldReturnBadRequestForIDMinus1()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var id = -1;

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.GetByID(id);

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
            var repo = new CouponCodeRepository(dbContext);

            var id = 100;

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.GetByID(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
