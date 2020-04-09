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
    public class ModifyActionTests
    {
        public CouponCodeModel CreateModel() => new CouponCodeModel()
        {
            Code = "KRISZW",
            DiscountID = 1,
            StartDate = DateTime.Now.AddDays(-30),
            EndDate = DateTime.Now.AddDays(60),
            IsLimitedForUsers = true,
        };

        [Fact]
        public async void Modify_ShouldReturnSuccess()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var id = 3;
            var model = CreateModel();

            var expectedEndDate = model.EndDate;
            var expecetedUsersSize = 1;

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.Modify(id, model);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
            Assert.Equal(expectedEndDate, actual.EndDate);
            Assert.Equal(expecetedUsersSize, actual.Users.Count);
        }

        [Fact]
        public async void Modify_ShouldReturnBadRequestForIDMinus1()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var id = -1;
            var model = CreateModel();

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.Modify(id, model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Modify_ShouldReturnBadRequestForCouponCodeIsEmptyString()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var id = 3;
            var model = CreateModel();
            model.Code = string.Empty;

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.Modify(id, model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Modify_ShouldReturnBadRequestForInvalidModelState()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var id = 3;
            var model = CreateModel();
            model.Code = default;

            //Act
            var controller = new CouponCodeController(repo, default);
            controller.ModelState.AddModelError("Code", "A code nem lehet üres");
            var actionResult = await controller.Modify(id, model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Modify_ShouldReturnNotFoundForID100()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var id = 100;
            var model = CreateModel();

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.Modify(id, model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
