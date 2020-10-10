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
    public class ModifyMethodTests
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
        public async void Modify_ShouldReturnOneCouponCodeWithCODEKRISZW()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var id = 3;
            var model = CreateModel();
            model.ID = id;

            var expectedCouponCode = "KRISZW";
            var expectedUsersSize = 1;
            var expectedEndDate = model.EndDate;

            //Act
            await repo.Update(id,model);
            var updatedItem = await repo.GetByID(id);

            //Assert
            Assert.NotNull(updatedItem);
            Assert.Equal(expectedCouponCode, updatedItem.Code);
            Assert.Equal(expectedUsersSize, updatedItem.Users.Count);
            Assert.Equal(expectedEndDate,updatedItem.EndDate);
        }

        [Fact]
        public async void Modify_ShouldThrowCouponCodeNotFoundExceptionForID100()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var id = 100;
            var model = CreateModel();


            //Act
            var uploadTask = repo.Update(id, model);

            //Assert
            await Assert.ThrowsAsync<CouponCodeWithIDNotFoundException>(() => uploadTask);
        }
    }
}
