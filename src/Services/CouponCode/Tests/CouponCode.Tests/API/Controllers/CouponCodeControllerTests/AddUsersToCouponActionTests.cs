using CouponCode.API.Controllers;
using CouponCode.API.Data;
using CouponCode.API.Exceptions;
using CouponCode.API.Services.Repositories.Implementations;
using CouponCode.Tests.FakeImplementation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CouponCode.Tests.API.Controllers.CouponCodeControllerTests
{
    public class AddUsersToCouponActionTests
    {
        [Fact]
        public async void AddUsersToCoupon_ShouldReturnOneCouponCodeWithCODEKRISZWAnd2Users()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var couponCode = "KRISZW";
            var userIDs = new List<string>()
            {
                "testusersada"
            };

            var expectedCouponCode = "KRISZW";
            var expectedUsersSize = 2;

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.AddUsersToCoupon(couponCode, userIDs);
            var actual = await repo.GetByCodeName(couponCode);

            //Assert
            Assert.NotNull(actual);
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
            Assert.Equal(expectedCouponCode, actual.Code);
            Assert.Equal(expectedUsersSize, actual.Users.Count);
        }

        [Fact]
        public async void AddUsersToCoupon_ShouldReturnOneCouponCodeWithEnabledUsersLimitationAndWith1User()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var couponCode = "BLCKFRDY2021";
            var userIDs = new List<string>()
            {
                "testusersada"
            };

            var expectedCouponCode = "BLCKFRDY2021";
            var expectedUsersLimitation = true;
            var expectedUsersSize = 1;

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.AddUsersToCoupon(couponCode, userIDs);
            var actual = await repo.GetByCodeName(couponCode);

            //Assert
            Assert.NotNull(actual);
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
            Assert.Equal(expectedUsersLimitation,actual.IsLimitedForUsers);
            Assert.Equal(expectedCouponCode, actual.Code);
            Assert.Equal(expectedUsersSize, actual.Users.Count);
        }

        [Fact]
        public async void AddUsersToCoupon_ShouldReturnBadRequestForEmptyCouponCode()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var couponCode = string.Empty;
            var userIDs = new List<string>()
            {
                "testusersada"
            };

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.AddUsersToCoupon(couponCode, userIDs);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void AddUsersToCoupon_ShouldReturnBadRequestForEmptyUserIDs()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var couponCode = "asdadsada";
            var userIDs = new List<string>();

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.AddUsersToCoupon(couponCode, userIDs);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void AddUsersToCoupon_ShouldReturnBadRequestForUserIDsIsNull()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var couponCode = "asdadsada";
            var userIDs = default(List<string>);

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.AddUsersToCoupon(couponCode, userIDs);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void AddUsersToCoupon_ShouldReturnNotFound()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var couponCode = "asdadsada";
            var userIDs = new List<string>()
            {
                "testusersada"
            };

            //Act
            var controller = new CouponCodeController(repo, default);
            var actionResult = await controller.AddUsersToCoupon(couponCode, userIDs);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
