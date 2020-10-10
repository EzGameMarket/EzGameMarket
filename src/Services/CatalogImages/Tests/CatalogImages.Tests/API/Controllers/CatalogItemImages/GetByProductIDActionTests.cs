using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using CatalogImages.Tests.FakeImplementations;
using CatalogImages.API.Data;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CatalogImages.API.Models;

namespace CatalogImages.Tests.API.Controllers.CatalogItemImages
{
    public class GetByProductIDActionTests
    {
        [Fact]
        public async void GetByID_ShouldReturnSuccessForCSGO()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var service = new CatalogItemImageService(dbContext, default, default);

            var productID = "csgo";
            var expectedItemsSize = 2;

            //Act
            var controller = new CatalogItemImagesController(service);
            var actionResult = await controller.GetImagesForProductID(productID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<List<CatalogItemImageModel>>>(actionResult);
            var value = Assert.IsAssignableFrom<List<CatalogItemImageModel>>(actionResult.Value);
            Assert.NotNull(value);
            Assert.Equal(expectedItemsSize,value.Count);
        }

        [Fact]
        public async void GetByID_ShouldReturnBadRequestForEmptyStringProductID()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var service = new CatalogItemImageService(dbContext, default, default);

            var productID = string.Empty;

            //Act
            var controller = new CatalogItemImagesController(service);
            var actionResult = await controller.GetImagesForProductID(productID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void GetByID_ShouldReturnNotFoundForHL2()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var service = new CatalogItemImageService(dbContext, default, default);

            var productID = "hl2";

            //Act
            var controller = new CatalogItemImagesController(service);
            var actionResult = await controller.GetImagesForProductID(productID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
