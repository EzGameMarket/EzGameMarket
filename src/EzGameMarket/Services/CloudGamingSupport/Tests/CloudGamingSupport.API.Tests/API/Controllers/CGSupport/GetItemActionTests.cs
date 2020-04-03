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
    public class GetItemActionTests
    {
        FakeCGDbContext _dbContext = new FakeCGDbContext();

        [Fact]
        public async void Get_ShouldReturnSuccessAndAnd1Item()
        {
            //Arange
            var dbContext = new FakeCGDbContext();
            var providerRepo = new CloudGamingProviderRepository(dbContext.DbContext);
            var repo = new CloudGamingSupportRepository(dbContext.DbContext, providerRepo);

            var id = 1;

            //Act
            var controller = new CGSupportController(repo);
            var actionResult = await controller.Get(id);
            var item = await repo.Get(id);

            //Assert
            Assert.NotNull(item);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<CloudGamingSupported>>(actionResult);
            var value = Assert.IsAssignableFrom<CloudGamingSupported>(actionResult.Value);
            Assert.NotNull(value);
        }

        [Fact]
        public async void Get_ShouldReturnBadRequestForIDMinus1()
        {
            //Arange
            var dbContext = new FakeCGDbContext();
            var providerRepo = new CloudGamingProviderRepository(dbContext.DbContext);
            var repo = new CloudGamingSupportRepository(dbContext.DbContext, providerRepo);

            var id = -1;

            //Act
            var controller = new CGSupportController(repo);
            var actionResult = await controller.Get(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void Get_ShouldReturnNotFoundForID200()
        {
            //Arange
            var dbContext = new FakeCGDbContext();
            var providerRepo = new CloudGamingProviderRepository(dbContext.DbContext);
            var repo = new CloudGamingSupportRepository(dbContext.DbContext, providerRepo);

            var id = 200;

            //Act
            var controller = new CGSupportController(repo);
            var actionResult = await controller.Get(id);
            var item = await repo.Get(id);

            //Assert
            Assert.Null(item);
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
