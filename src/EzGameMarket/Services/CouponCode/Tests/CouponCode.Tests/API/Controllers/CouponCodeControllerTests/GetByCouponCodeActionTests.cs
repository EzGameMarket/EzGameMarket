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
    public class GetByCouponCodeActionTests
    {
        [Fact]
        public async void GetByCouponCode_ShouldReturnSuccess()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var code = "KRISZW";

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.GetByCode(code);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<CouponCodeModel>>(actionResult);
            var value = Assert.IsAssignableFrom<CouponCodeModel>(actionResult.Value);
            Assert.NotNull(value);
            Assert.Equal(code, value.Code);
        }

        [Fact]
        public async void GetByCouponCode_ShouldReturnBadRequestForIDMinus1()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var code = string.Empty;

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.GetByCode(code);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void GetByCouponCode_ShouldReturnNotFoundForID100()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var code = "asdaad";

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.GetByCode(code);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
