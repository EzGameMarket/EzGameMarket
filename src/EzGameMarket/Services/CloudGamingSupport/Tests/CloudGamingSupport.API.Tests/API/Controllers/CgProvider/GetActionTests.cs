using CloudGamingSupport.API.Controllers;
using CloudGamingSupport.API.Models;
using CloudGamingSupport.API.Services.Repositories.Implementations;
using CloudGamingSupport.API.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CloudGamingSupport.API.Tests.API.Controllers.CgProvider
{
    public class GetActionTests
    {
        FakeCGDbContext _dbContext = new FakeCGDbContext();

        [Fact]
        public async void Get_ShouldReturnSuccessAnd2ProviderForR6S()
        {
            //Arange
            var dbContext = new FakeCGDbContext();
            var repo = new CloudGamingProviderRepository(dbContext.DbContext);

            var id = 1;

            //Act
            var controller = new CgProviderController(repo);
            var actionResult = await controller.Get(id);
            var item = await repo.Get(id);

            //Assert
            Assert.NotNull(item);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<CloudGamingProvider>>(actionResult);
            var value = Assert.IsAssignableFrom<CloudGamingProvider>(actionResult.Value);
            Assert.NotNull(value);
            Assert.Equal(item.Name, value.Name);
        }

        [Fact]
        public async void Get_ShouldReturnBadRequestForIDMinus1()
        {
            //Arange
            var dbContext = new FakeCGDbContext();
            var repo = new CloudGamingProviderRepository(dbContext.DbContext);

            var id = -1;

            //Act
            var controller = new CgProviderController(repo);
            var actionResult = await controller.Get(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void Get_ShouldReturnNotFoundForProductIDHL2()
        {
            //Arange
            var dbContext = new FakeCGDbContext();
            var repo = new CloudGamingProviderRepository(dbContext.DbContext);

            var id = 5;

            //Act
            var controller = new CgProviderController(repo);
            var actionResult = await controller.Get(id);
            var item = await repo.Get(id);

            //Assert
            Assert.Null(item);
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
