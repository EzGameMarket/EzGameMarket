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
    public class ModifyActionTests
    {
        FakeCGDbContext _dbContext = new FakeCGDbContext();

        private CloudGamingSupported CreateModel() => new CloudGamingSupported()
        {
            ID = 3,
            ProductID = "cod",
        };

        [Fact]
        public async void Modify_ModifiedItemShouldReturnSuccess()
        {
            //Arange
            var dbContext = new FakeCGDbContext();
            var providerRepo = new CloudGamingProviderRepository(dbContext.DbContext);
            var repo = new CloudGamingSupportRepository(dbContext.DbContext, providerRepo);

            var model = CreateModel();
            var id = model.ID.GetValueOrDefault();

            //Act
            var controller = new CGSupportController(repo);
            var actionResult = await controller.Modify(id,model);
            var item = await repo.Get(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
            Assert.NotNull(item);
            Assert.Equal(model.ProductID, item.ProductID);
        }

        [Fact]
        public async void Modify_ShouldReturnBadReqeustForInvalidModel()
        {
            //Arange
            var dbContext = new FakeCGDbContext();
            var providerRepo = new CloudGamingProviderRepository(dbContext.DbContext);
            var repo = new CloudGamingSupportRepository(dbContext.DbContext, providerRepo);

            var model = CreateModel();
            model.ProductID = default;

            //Act
            var controller = new CGSupportController(repo);
            controller.ModelState.AddModelError("ProductID", "A ProductID nem lehet null");
            var actionResult = await controller.Modify(model.ID.GetValueOrDefault(),model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Modify_ShouldReturnBadReqeustForIDMinus1()
        {
            //Arange
            var dbContext = new FakeCGDbContext();
            var providerRepo = new CloudGamingProviderRepository(dbContext.DbContext);
            var repo = new CloudGamingSupportRepository(dbContext.DbContext, providerRepo);

            var id = -1;

            var model = CreateModel();
            model.ID = id;

            //Act
            var controller = new CGSupportController(repo);
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
            var providerRepo = new CloudGamingProviderRepository(dbContext.DbContext);
            var repo = new CloudGamingSupportRepository(dbContext.DbContext, providerRepo);

            var id = 200;

            var model = CreateModel();
            model.ID = id;


            //Act
            var controller = new CGSupportController(repo);
            var actionResult = await controller.Modify(id,model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
