using CouponCode.API.Data;
using CouponCode.API.Exceptions.CouponCode;
using CouponCode.API.Models;
using CouponCode.API.Services.Repositories.Implementations;
using CouponCode.Tests.FakeImplementation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CouponCode.Tests.API.Repositories.CouponCode
{
    public class AddUsersMethodTests
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
            await repo.AddUsersToCoupon(couponCode,userIDs);
            var actual = await repo.GetByCodeName(couponCode);

            //Assert
            Assert.NotNull(actual);
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
            var expecetedUsersLimitation = true;
            var expecetedUsersSize = 1;

            //Act
            await repo.AddUsersToCoupon(couponCode, userIDs);
            var actual = await repo.GetByCodeName(couponCode);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedCouponCode, actual.Code);
            Assert.Equal(expecetedUsersSize, actual.Users.Count);
            Assert.Equal(expecetedUsersLimitation, actual.IsLimitedForUsers);
        }


        [Fact]
        public async void AddUsersToCoupon_ShouldThrowCouponCodeAlreadyUploadedExcption()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var couponCode = "asdadsada";

            //Act
            var uploadTask = repo.AddUsersToCoupon(couponCode,default);

            //Assert
            await Assert.ThrowsAsync<CouponCodeNotFoundException>(() => uploadTask);
        }
    }
}
