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
    public class GetByCouponCodeMethodTests
    {
        [Fact]
        public async void GetByCouponCode_ShouldReturnOneCouponCodeWithCODEKRISZW()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var code = "KRISZW";

            //Act
            var value = await repo.GetByCodeName(code);

            //Assert
            Assert.NotNull(value);
            Assert.Equal(code, value.Code);
        }

        [Fact]
        public async void GetByCouponCode_ShouldReturnNull()
        {
            //Arrange 
            var dbOptions = FakeDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CouponCodeDbContext(dbOptions);
            var repo = new CouponCodeRepository(dbContext);

            var code = "asdaad";

            //Act
            var value = await repo.GetByCodeName(code);

            //Assert
            Assert.Null(value);
        }
    }
}
