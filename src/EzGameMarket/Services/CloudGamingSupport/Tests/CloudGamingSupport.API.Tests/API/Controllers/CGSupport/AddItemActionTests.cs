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
    public class AddItemActionTests
    {
        private CloudGamingSupported CreateModel() => new CloudGamingSupported()
        {
            ID = default,
            ProductID = "cod",
            Providers = new List<CloudGamingProvidersAndGames>()
            {
                FakeCGDbContext.CreateMatches()[0]
            }
        };

        [Fact]
        public async void Add_NewItemShouldReturnSuccess()
        {
            //Arange
            var dbContext = new FakeCGDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName+"-"+ System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.GUID.ToString());
            var providerRepo = new CloudGamingProviderRepository(dbContext.DbContext);
            var repo = new CloudGamingSupportRepository(dbContext.DbContext, providerRepo);

            var model = CreateModel();
            var expectedProviderItemsSize = 1;
            var id = 4;

            //Act
            var controller = new CGSupportController(repo);
            var actionResult = await controller.Add(model);
            var item = await repo.Get(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
            Assert.NotNull(item);
            Assert.Equal(model.ProductID, item.ProductID);
            Assert.Equal(expectedProviderItemsSize, item.Providers.Count);
        }

        [Fact]
        public async void Add_ShouldReturnBadReqeustForInvalidModel()
        {
            //Arange
            var dbContext = new FakeCGDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName+"-"+ System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.GUID.ToString());
            var providerRepo = new CloudGamingProviderRepository(dbContext.DbContext);
            var repo = new CloudGamingSupportRepository(dbContext.DbContext, providerRepo);

            var model = CreateModel();
            model.ProductID = default;

            //Act
            var controller = new CGSupportController(repo);
            controller.ModelState.AddModelError("ProductID","A ProductID nem lehet null");
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Add_ShouldReturnConflictForID1()
        {
            //Arange
            var dbContext = new FakeCGDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName+"-"+ System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.GUID.ToString());
            var providerRepo = new CloudGamingProviderRepository(dbContext.DbContext);
            var repo = new CloudGamingSupportRepository(dbContext.DbContext, providerRepo);

            var model = CreateModel();
            model.ID = 1;

            //Act
            var controller = new CGSupportController(repo);
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ConflictResult>(actionResult);
        }

        [Fact]
        public async void Add_ShouldReturnConflictForCSGO()
        {
            //Arange
            var dbContext = new FakeCGDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName+"-"+ System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.GUID.ToString());
            var providerRepo = new CloudGamingProviderRepository(dbContext.DbContext);
            var repo = new CloudGamingSupportRepository(dbContext.DbContext, providerRepo);

            var model = CreateModel();
            model.ProductID = "csgo";

            //Act
            var controller = new CGSupportController(repo);
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ConflictResult>(actionResult);
        }
    }
}
