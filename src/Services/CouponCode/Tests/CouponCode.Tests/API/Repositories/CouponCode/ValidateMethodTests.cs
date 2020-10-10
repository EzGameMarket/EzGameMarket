using CouponCode.API.Data;
using CouponCode.API.Exceptions.CouponCode;
using CouponCode.API.Exceptions.CouponCode.CouponCodeValidation;
using CouponCode.API.Services.Repositories.Implementations;
using CouponCode.API.ViewModels;
using CouponCode.Tests.FakeImplementation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CouponCode.Tests.API.Repositories.CouponCode
{
    public class ValidateMethodTests
    {
        private ValidateCouponCodeViewModel CreateModel() => new ValidateCouponCodeViewModel()
        {
            CouponCode = "KRISZW",
            UserID = "kriszw"
        };

        [Fact]
        public async void Validate_ShouldReturnOkay()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var model = CreateModel();

            //Act
            var result = await repo.Validate(model);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void Validate_ShouldReturnFalseForNotExistingCouponCode()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var model = CreateModel();
            model.CouponCode = "asd";

            //Act
            var validateTask = repo.Validate(model);

            //Assert
            await Assert.ThrowsAsync<CouponCodeNotFoundException>(() => validateTask);
        }

        [Fact]
        public async void Validate_ShouldReturnFalseForEmptyCouponCode()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var model = CreateModel();
            model.CouponCode = "";

            //Act
            var result = await repo.Validate(model);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async void Validate_ShouldReturnFalseForEmptyUserID()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var model = CreateModel();
            model.UserID = "";

            //Act
            var result = await repo.Validate(model);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async void Validate_ShouldReturnFalseForOutDatedCouponCode()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var model = CreateModel();
            model.CouponCode = "OUTDATED";

            //Act
            var validateTask = repo.Validate(model);

            //Assert
            await Assert.ThrowsAsync<CouponCodeOutdatedException>(() => validateTask);
        }

        [Fact]
        public async void Validate_ShouldReturnFalseForLimitedCouponCodeAndUserNotEligible()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var model = CreateModel();
            model.UserID = "test";

            //Act
            var validateTask = repo.Validate(model);

            //Assert
            await Assert.ThrowsAsync<UserNotEligibleForCouponCodeException>(() => validateTask);
        }
    }
}
