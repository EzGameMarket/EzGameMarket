using CouponCode.API.Controllers;
using CouponCode.API.Data;
using CouponCode.API.Models;
using CouponCode.API.Services.Repositories.Implementations;
using CouponCode.API.Services.Service.Abstractions;
using CouponCode.API.ViewModels;
using CouponCode.Tests.FakeImplementation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CouponCode.Tests.API.Controllers.CouponCodeControllerTests
{
    public class ValidateCouponCodeActionTests
    {
        private ValidateCouponCodeViewModel CreateModel() => new ValidateCouponCodeViewModel()
        {
            CouponCode = "KRISZW",
            UserID = "kriszw"
        };

        [Fact]
        public async void Validate_ShouldReturnSuccess()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var model = CreateModel();

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.ValidateCouponCode(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<AcceptedResult>(actionResult);
        }

        [Fact]
        public async void Validate_ShouldReturnSuccessForEmptyUserIDBecauseCurrentLoggedInUserIsKriszW()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);
            var identityServiceMocked = new Mock<IIdentityService>();
            identityServiceMocked.Setup(x => x.GetUserID(default)).Returns("kriszw");

            var model = CreateModel();
            model.UserID = "";

            //Act
            var controller = new CouponCodeController(repo, identityServiceMocked.Object);
            var actionResult = await controller.ValidateCouponCode(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<AcceptedResult>(actionResult);
        }

        [Fact]
        public async void Validate_ShouldReturnBadRequestForEmptyCouponCode()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var model = CreateModel();
            model.CouponCode = "";

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.ValidateCouponCode(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Validate_ShouldReturnNotFoundForNotExistingCouponCode()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var model = CreateModel();
            model.CouponCode = "asdasda";

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.ValidateCouponCode(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void Validate_ShouldReturnConflictForOutdatedCouponCode()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var model = CreateModel();
            model.CouponCode = "OUTDATED";

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.ValidateCouponCode(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ConflictResult>(actionResult);
        }

        [Fact]
        public async void Validate_ShouldReturnForbidForNotEligibleUserID()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var model = CreateModel();
            model.UserID = "test";

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.ValidateCouponCode(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ForbidResult>(actionResult);
        }
    }
}
