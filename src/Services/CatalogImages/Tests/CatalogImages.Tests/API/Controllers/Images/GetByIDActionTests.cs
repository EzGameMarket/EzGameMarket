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

namespace CatalogImages.Tests.API.Controllers.Images
{
    public class GetByIDActionTests
    {
        [Fact]
        public async void GetImage_ShouldReturnSuccessForID1()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var repo = new CatalogImageRepository(dbContext, default, default);
            var service = new CatalogItemImageService(dbContext, default, default);

            var id = 1;

            //Act
            var controller = new ImagesController(service, repo, default);
            var actionResult = await controller.GetByID(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<CatalogItemImageModel>>(actionResult);
            var actual = Assert.IsAssignableFrom<CatalogItemImageModel>(actionResult.Value);
            Assert.NotNull(actual);
        }

        [Fact]
        public async void GetImage_ShouldReturnBadRequestForIDMinus1()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var repo = new CatalogImageRepository(dbContext,default,default);
            var service = new CatalogItemImageService(dbContext, default, default);

            var id = -1;

            //Act
            var controller = new ImagesController(service,repo, default);
            var actionResult = await controller.GetByID(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void GetImage_ShouldReturnNotFoundForID100()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var repo = new CatalogImageRepository(dbContext,default,default);
            var service = new CatalogItemImageService(dbContext, default, default);

            var id = 100;

            //Act
            var controller = new ImagesController(service, repo, default);
            var actionResult = await controller.GetByID(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
