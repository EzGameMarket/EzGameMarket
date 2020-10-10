using CloudGamingSupport.API.Controllers;
using CloudGamingSupport.API.Models;
using CloudGamingSupport.API.Services.Repositories.Implementations;
using CloudGamingSupport.API.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CloudGamingSupport.API.Tests.API.Controllers.CGSupport
{
    public class GetFromProductIDActionTests
    {
        

        [Fact]
        public async void GetFromProductID_ShouldReturnSuccessAnd2ProviderForR6S()
        {
            //Arange
            var dbContext = new FakeCGDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName+"-"+ System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.GUID.ToString());
            var providerRepo = new CloudGamingProviderRepository(dbContext.DbContext);
            var repo = new CloudGamingSupportRepository(dbContext.DbContext, providerRepo);

            var productID = "r6s";

            var expectedProvidersCount = 2;

            //Act
            var controller = new CGSupportController(repo);
            var actionResult = await controller.GetFromProductID(productID);
            var item = await repo.GetFromProductID(productID);

            //Assert
            Assert.NotNull(item);
            Assert.Equal(expectedProvidersCount, item.Providers.Count);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<CloudGamingSupported>>(actionResult);
            var value = Assert.IsAssignableFrom<CloudGamingSupported>(actionResult.Value);
            Assert.NotNull(value);
            Assert.Equal(expectedProvidersCount,value.Providers.Count);
        }

        [Fact]
        public async void GetFromProductID_ShouldReturnBadRequestForEmptyString()
        {
            //Arange
            var dbContext = new FakeCGDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName+"-"+ System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.GUID.ToString());
            var providerRepo = new CloudGamingProviderRepository(dbContext.DbContext);
            var repo = new CloudGamingSupportRepository(dbContext.DbContext, providerRepo);

            var productID = string.Empty;

            //Act
            var controller = new CGSupportController(repo);
            var actionResult = await controller.GetFromProductID(productID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void GetFromProductID_ShouldReturnNotFoundForProductIDHL2()
        {
            //Arange
            var dbContext = new FakeCGDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName+"-"+ System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.GUID.ToString());
            var providerRepo = new CloudGamingProviderRepository(dbContext.DbContext);
            var repo = new CloudGamingSupportRepository(dbContext.DbContext, providerRepo);

            var productID = "hl2";

            //Act
            var controller = new CGSupportController(repo);
            var actionResult = await controller.GetFromProductID(productID);
            var item = await repo.GetFromProductID(productID);

            //Assert
            Assert.Null(item);
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
