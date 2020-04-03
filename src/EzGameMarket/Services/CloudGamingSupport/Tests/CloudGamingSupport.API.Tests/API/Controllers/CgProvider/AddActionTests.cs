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
    public class AddActionTests
    {
        FakeCGDbContext _dbContext = new FakeCGDbContext();

        private CloudGamingProvider CreateModel() => new CloudGamingProvider()
        {
            ID = default,
            Name = "Playstation Now",
            SearchURl = "https://www.psnow.com/search",
            Url = "https://www.psnow.com",
            SupportedGames = new List<CloudGamingProvidersAndGames>()
        };

        [Fact]
        public async void Add_NewItemShouldReturnSuccess()
        {
            //Arange
            var dbContext = new FakeCGDbContext();
            var repo = new CloudGamingProviderRepository(dbContext.DbContext);
            var model = CreateModel();
            var id = 3;

            //Act
            var controller = new CgProviderController(repo);
            var actionResult = await controller.Add(model);
            var item = await repo.Get(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
            Assert.NotNull(item);
            Assert.Equal(model.Name,item.Name);
        }

        [Fact]
        public async void Add_ShouldReturnBadReqeustForInvalidModel()
        {
            //Arange
            var dbContext = new FakeCGDbContext();
            var repo = new CloudGamingProviderRepository(dbContext.DbContext);

            var model = CreateModel();
            model.Name = default;

            //Act
            var controller = new CgProviderController(repo);
            controller.ModelState.AddModelError("Name", "A szolgáltató neve nem lehet null");
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Add_ShouldReturnConflictForID1()
        {
            //Arange
            var dbContext = new FakeCGDbContext();
            var repo = new CloudGamingProviderRepository(dbContext.DbContext);

            var model = CreateModel();
            model.ID = 1;

            //Act
            var controller = new CgProviderController(repo);
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ConflictResult>(actionResult);
        }

        [Fact]
        public async void Add_ShouldReturnConflictForStadia()
        {
            //Arange
            var dbContext = new FakeCGDbContext();
            var repo = new CloudGamingProviderRepository(dbContext.DbContext);

            var model = CreateModel();
            model.Name = FakeCGDbContext.Providers[0].Name;

            //Act
            var controller = new CgProviderController (repo);
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ConflictResult>(actionResult);
        }
    }
}
