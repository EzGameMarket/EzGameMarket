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
    public class ModifyActionTests
    {
        FakeCGDbContext _dbContext = new FakeCGDbContext();

        private CloudGamingProvider CreateModel() => new CloudGamingProvider()
        {
            ID = 1,
            Name = "NVIDIA GeForce NOW",
            SearchURl = "https://www.gamewatcher.com/news/nvidia-geforce-now-games-list/search",
            Url = "https://www.nvidia.com/en-eu/geforce-now/",
            SupportedGames = new List<CloudGamingProvidersAndGames>()
            {
                FakeCGDbContext.Matches[0],
                FakeCGDbContext.Matches[1],
            }
        };

        [Fact]
        public async void Modify_ModifiedItemShouldReturnSuccess()
        {
            //Arange
            var dbContext = new FakeCGDbContext();
            var repo = new CloudGamingProviderRepository(dbContext.DbContext);

            var model = CreateModel();
            var id = model.ID.GetValueOrDefault();

            //Act
            var controller = new CgProviderController(repo);
            var actionResult = await controller.Modify(id, model);
            var item = await repo.Get(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
            Assert.NotNull(item);
            Assert.Equal(model.Name, item.Name);
        }

        [Fact]
        public async void Modify_ShouldReturnBadReqeustForInvalidModel()
        {
            //Arange
            var dbContext = new FakeCGDbContext();
            var repo = new CloudGamingProviderRepository(dbContext.DbContext);

            var model = CreateModel();
            model.Name = default;

            //Act
            var controller = new CgProviderController(repo);
            controller.ModelState.AddModelError("Name", "A Name nem lehet null");
            var actionResult = await controller.Modify(model.ID.GetValueOrDefault(), model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Modify_ShouldReturnBadReqeustForIDMinus1()
        {
            //Arange
            var dbContext = new FakeCGDbContext();
            var repo = new CloudGamingProviderRepository(dbContext.DbContext);

            var id = -1;

            var model = CreateModel();
            model.ID = id;

            //Act
            var controller = new CgProviderController(repo);
            var actionResult = await controller.Modify(id, model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Modify_ShouldReturnNotFoundForID200()
        {
            //Arange
            var dbContext = new FakeCGDbContext();
            var repo = new CloudGamingProviderRepository(dbContext.DbContext);

            var id = 200;

            var model = CreateModel();
            model.ID = id;


            //Act
            var controller = new CgProviderController(repo);
            var actionResult = await controller.Modify(id, model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
