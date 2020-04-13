using CatalogImages.API.Controllers;
using CatalogImages.API.Data;
using CatalogImages.API.Models;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Controllers.CatalogItemImages
{
    public class GetByProductIDWithFIlteringActionTests
    {
        [Fact]
        public async void GetByIDWithFiltering_ShouldReturnSuccessForCSGO()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var repo = new CatalogItemImageService(dbContext);

            var productID = "csgo";
            var expectedItemsSize = 1;
            var typeName = "Catalog";
            var sizeName = default(string);

            //Act
            var controller = new CatalogItemImagesController(repo);
            var actionResult = await controller.GetImagesForProductIDWithFiltering(productID, typeName, sizeName);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<List<CatalogItemImageModel>>>(actionResult);
            var value = Assert.IsAssignableFrom<List<CatalogItemImageModel>>(actionResult.Value);
            Assert.NotNull(value);
            Assert.Equal(expectedItemsSize, value.Count);
        }

        [Fact]
        public async void GetByIDWithFiltering_ShouldReturnBadRequestForEmptyStringProductID()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var repo = new CatalogItemImageService(dbContext);

            var productID = string.Empty;
            var typeName = "Catalog";
            var sizeName = default(string);

            //Act
            var controller = new CatalogItemImagesController(repo);
            var actionResult = await controller.GetImagesForProductIDWithFiltering(productID, typeName, sizeName);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void GetByIDWithFiltering_ShouldReturnNotFoundForHL2()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var repo = new CatalogItemImageService(dbContext);

            var productID = "hl2";
            var typeName = "Catalog";
            var sizeName = default(string);

            //Act
            var controller = new CatalogItemImagesController(repo);
            var actionResult = await controller.GetImagesForProductIDWithFiltering(productID, typeName, sizeName);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
