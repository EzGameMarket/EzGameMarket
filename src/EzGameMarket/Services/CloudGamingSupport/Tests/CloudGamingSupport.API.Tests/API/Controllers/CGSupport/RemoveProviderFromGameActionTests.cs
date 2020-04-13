﻿using CloudGamingSupport.API.Controllers;
using CloudGamingSupport.API.Services.Repositories.Implementations;
using CloudGamingSupport.API.Tests.FakeImplementations;
using CloudGamingSupport.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CloudGamingSupport.API.Tests.API.Controllers.CGSupport
{
    public class RemoveProviderFromGameActionTests
    {
        private ProviderModifyForGameViewModel CreateModel() => new ProviderModifyForGameViewModel()
        {
            ProductID = "csgo",
            ProviderID = 1
        };

        [Fact]
        public async void AddProviderForGame_ShouldReturnSuccessAnd0ProviderForCSGO()
        {
            //Arange
            var dbContext = new FakeCGDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + "-" + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.GUID.ToString());
            var providerRepo = new CloudGamingProviderRepository(dbContext.DbContext);
            var repo = new CloudGamingSupportRepository(dbContext.DbContext, providerRepo);

            var model = CreateModel();
            var expectedProviderItemsSize = 0;

            //Act
            var controller = new CGSupportController(repo);
            var actionResult = await controller.RemoveProviderFromGame(model);
            var item = await repo.GetFromProductID(model.ProductID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
            Assert.NotNull(item);
            Assert.Equal(expectedProviderItemsSize, item.Providers.Count);
        }

        [Fact]
        public async void AddProviderForGame_ShouldReturnBadRequestForProviderIDMinus1()
        {
            //Arange
            var dbContext = new FakeCGDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + "-" + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.GUID.ToString());
            var providerRepo = new CloudGamingProviderRepository(dbContext.DbContext);
            var repo = new CloudGamingSupportRepository(dbContext.DbContext, providerRepo);

            var model = CreateModel();
            model.ProviderID = -1;

            //Act
            var controller = new CGSupportController(repo);
            var actionResult = await controller.RemoveProviderFromGame(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void AddProviderForGame_ShouldReturnBadRequestForEmptyStringProductID()
        {
            //Arange
            var dbContext = new FakeCGDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + "-" + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.GUID.ToString());
            var providerRepo = new CloudGamingProviderRepository(dbContext.DbContext);
            var repo = new CloudGamingSupportRepository(dbContext.DbContext, providerRepo);

            var model = CreateModel();
            model.ProductID = string.Empty;

            //Act
            var controller = new CGSupportController(repo);
            var actionResult = await controller.RemoveProviderFromGame(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void AddProviderForGame_ShouldReturnBadRequestForInvalidModelState()
        {
            //Arange
            var dbContext = new FakeCGDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + "-" + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.GUID.ToString());
            var providerRepo = new CloudGamingProviderRepository(dbContext.DbContext);
            var repo = new CloudGamingSupportRepository(dbContext.DbContext, providerRepo);

            var model = CreateModel();
            model.ProductID = string.Empty;

            //Act
            var controller = new CGSupportController(repo);
            controller.ModelState.AddModelError("ProductID", "A product id nem lehet üres");
            var actionResult = await controller.RemoveProviderFromGame(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void AddProviderForGame_ShouldReturnConflictCSGOAndProviderID2()
        {
            //Arange
            var dbContext = new FakeCGDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + "-" + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.GUID.ToString());
            var providerRepo = new CloudGamingProviderRepository(dbContext.DbContext);
            var repo = new CloudGamingSupportRepository(dbContext.DbContext, providerRepo);

            var model = CreateModel();
            model.ProductID = "csgo";
            model.ProviderID = 2;

            //Act
            var controller = new CGSupportController(repo);
            var actionResult = await controller.RemoveProviderFromGame(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ConflictResult>(actionResult);
        }

        [Fact]
        public async void AddProviderForGame_ShouldReturnNotFoundForProductIDDoom()
        {
            //Arange
            var dbContext = new FakeCGDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + "-" + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.GUID.ToString());
            var providerRepo = new CloudGamingProviderRepository(dbContext.DbContext);
            var repo = new CloudGamingSupportRepository(dbContext.DbContext, providerRepo);

            var model = CreateModel();
            model.ProductID = "doom";

            //Act
            var controller = new CGSupportController(repo);
            var actionResult = await controller.RemoveProviderFromGame(model);
            var item = await repo.GetFromProductID(model.ProductID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
            Assert.Null(item);
        }
        [Fact]
        public async void AddProviderForGame_ShouldReturnNotFoundForProviderID5()
        {
            //Arange
            var dbContext = new FakeCGDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + "-" + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.GUID.ToString());
            var providerRepo = new CloudGamingProviderRepository(dbContext.DbContext);
            var repo = new CloudGamingSupportRepository(dbContext.DbContext, providerRepo);

            var model = CreateModel();
            model.ProviderID = 5;

            //Act
            var controller = new CGSupportController(repo);
            var actionResult = await controller.RemoveProviderFromGame(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
