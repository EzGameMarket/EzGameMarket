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
    public class AddActionTests
    {
        public CouponCodeModel CreateModel() => new CouponCodeModel()
        {
            Code = "TESTADDCODE",
            DiscountID = 1,
            StartDate = DateTime.Now.AddDays(-10),
            EndDate = DateTime.Now.AddDays(20),
            IsLimitedForUsers = false,
        };

        [Fact]
        public async void Add_ShouldReturnSuccess()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var model = CreateModel();

            var expecetedUsersSize = 0;
            var expectedLimitedForUser = false;
            var expextedEndData = model.EndDate;

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.Add(model);
            var actual = await repo.GetByCodeName(model.Code);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
            Assert.Equal(expectedLimitedForUser, actual.IsLimitedForUsers);
            Assert.Equal(expecetedUsersSize, actual.Users.Count);
            Assert.Equal(expextedEndData, actual.EndDate);
        }

        [Fact]
        public async void Add_ShouldReturnSuccessAnd1UserWithIsLimitedForUsersTrue()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var model = CreateModel();
            model.Users = new List<EligibleUserModel>()
            {
                new EligibleUserModel()
                {
                    UserID = "asd"
                }
            };

            var expecetedUsersSize = 1;
            var expectedLimitedForUser = true;
            var expextedEndData = model.EndDate;

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.Add(model);
            var actual = await repo.GetByCodeName(model.Code);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
            Assert.Equal(expectedLimitedForUser, actual.IsLimitedForUsers);
            Assert.Equal(expecetedUsersSize, actual.Users.Count);
            Assert.Equal(expextedEndData, actual.EndDate);
        }

        [Fact]
        public async void Add_ShouldReturnBadRequestForIDMinus1()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var model = CreateModel();
            model.ID = -1;

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Add_ShouldReturnBadRequestForCouponCodeIsEmptyString()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var model = CreateModel();
            model.Code = string.Empty;

            //Act
            var controller = new CouponCodeController(repo, default);
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
            var repo = new CouponCodeRepository(dbContext);

            var model = CreateModel();
            model.Code = default;

            //Act
            var controller = new CouponCodeController(repo, default);
            controller.ModelState.AddModelError("Code", "A code nem lehet üres");
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Add_ShouldReturnConflictForID1()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var model = CreateModel();
            model.ID = 1;

            var expectedErrorMSG = "CouponCode ID";

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            var actual = Assert.IsAssignableFrom<ConflictObjectResult>(actionResult);
            Assert.Equal(expectedErrorMSG, actual.Value.ToString());
        }

        [Fact]
        public async void Add_ShouldReturnConflictForCouponKRISZW()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var model = CreateModel();
            model.Code = "KRISZW";

            var expectedErrorMSG = "CouponCode";

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            var actual = Assert.IsAssignableFrom<ConflictObjectResult>(actionResult);
            Assert.Equal(expectedErrorMSG,actual.Value.ToString());
        }
    }
}
