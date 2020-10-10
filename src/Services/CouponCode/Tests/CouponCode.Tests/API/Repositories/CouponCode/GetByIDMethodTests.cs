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

namespace CouponCode.Tests.API.Repositories.CouponCode
{
    public class GetByIDMethodTests
    {
        [Fact]
        public async void GetByID_ShouldReturnCouponCodeForID1()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var id = 1;

            //Act
            var item = await repo.GetByID(id);

            //Assert
            Assert.NotNull(item);
            Assert.Equal(id, item.ID);
        }

        [Fact]
        public async void GetByID_ShouldReturnNullForID100()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var id = 100;

            //Act
            var item = await repo.GetByID(id);

            //Assert
            Assert.Null(item);
        }
    }
}
