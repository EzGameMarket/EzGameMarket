using CouponCode.API.Data;
using CouponCode.API.Exceptions.CouponCode;
using CouponCode.API.Models;
using CouponCode.API.Services.Repositories.Implementations;
using CouponCode.Tests.FakeImplementation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CouponCode.Tests.API.Repositories.CouponCode
{
    public class AddMethodTests
    {
        public CouponCodeModel CreateModel() => new CouponCodeModel()
        {
            Code = "addTest",
            DiscountID = 1,
            EndDate = DateTime.Now.AddDays(30),
            StartDate = DateTime.Now,
            IsLimitedForUsers = false
        };

        [Fact]
        public async void Add_ShouldReturnOneCouponCodeWithCODEaddTest()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var model = CreateModel();

            var expectedCouponCode = "addTest";

            //Act
            await repo.Add(model);
            var actual = await repo.GetByCodeName(model.Code);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedCouponCode, actual.Code);
        }

        [Fact]
        public async void Add_ShouldReturnOneCouponCodeWithCODEaddTestAndIsLimitedForUsersTrueAnd1User()
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
                    UserID = "testasdaa"
                }
            };

            var expectedCouponCode = "addTest";
            var expectedLimitedForUsers = true;
            var expecetedUsersItemSize = 1;

            //Act
            await repo.Add(model);
            var actual = await repo.GetByCodeName(model.Code);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedCouponCode, actual.Code);
            Assert.Equal(expectedLimitedForUsers, actual.IsLimitedForUsers);
            Assert.Equal(expecetedUsersItemSize, actual.Users.Count);
        }

        [Fact]
        public async void Add_ShouldThrowCouponCodeAlreadyUploadedExcption()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var model = CreateModel();
            model.Code = "KRISZW";

            //Act
            var uploadTask = repo.Add(model);

            //Assert
            await Assert.ThrowsAsync<CouponCodeAlreadyUploadedException>(() => uploadTask);
        }
    }
}
