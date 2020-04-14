using CatalogImages.API.Controllers;
using CatalogImages.API.Data;
using CatalogImages.API.Models;
using CatalogImages.API.Services.Repositories.Abstractions;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Controllers.CatalogItemImages
{
    public class ChangeSizeActionTests
    {
        [Fact]
        public async void ChangeSize_ShouldReturnSuccessForID1()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var service = new CatalogItemImageService(dbContext, default, default);

            var id = 1;
            var sizeID = 2;

            var mockedImageSizeRepo = new Mock<IImageSizeRepository>();
            mockedImageSizeRepo.Setup(s => s.AnyWithID(sizeID)).ReturnsAsync(true);
            var repo = new CatalogImageRepository(dbContext, mockedImageSizeRepo.Object, default);


            //Act
            var controller = new CatalogItemImagesController(service, repo);
            var actionResult = await controller.ChangeSize(id, sizeID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
        }

        [Theory]
        [MemberData(nameof(CreateBadRequestTestData))]
        public async void ChangeSize_ShouldReturnBadRequest(int id, int sizeID, string expectedMSG)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{id}-{sizeID}-{expectedMSG}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var service = new CatalogItemImageService(dbContext, default, default);

            var mockedImageSizeRepo = new Mock<IImageSizeRepository>();
            mockedImageSizeRepo.Setup(s => s.AnyWithID(sizeID)).ReturnsAsync(true);
            var repo = new CatalogImageRepository(dbContext, mockedImageSizeRepo.Object, default);

            //Act
            var controller = new CatalogItemImagesController(service, repo);
            var actionResult = await controller.ChangeSize(id, sizeID);

            //Assert
            Assert.NotNull(actionResult);
            var actual = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);
            Assert.Equal(expectedMSG, actual.Value.ToString());
        }

        public static object[][] CreateBadRequestTestData() => new object[][]
        {
            new object[] { -1, 2, "A megadott kép azonosítója: -1 érvénytelen, nem lehet kisebb egynél" },
            new object[] { 1, -1, "A megadott kép méret azonosítója: -1 érvénytelen, nem lehet kisebb egynél" }
        };

        [Theory]
        [InlineData(100, 1, "Nem létezik feltöltött kép a 100 azonosítóval")]
        [InlineData(1, 100, "Nem létezik kép méret a 100 azonosítóval")]
        public async void ChangeSize_ShouldReturnNotFound(int productID, int sizeID, string expectedMSG)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{productID}-{sizeID}-{expectedMSG}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var service = new CatalogItemImageService(dbContext, default, default);

            var mockedImageSizeRepo = new Mock<IImageSizeRepository>();
            mockedImageSizeRepo.Setup(s => s.AnyWithID(sizeID)).ReturnsAsync(false);
            var repo = new CatalogImageRepository(dbContext, mockedImageSizeRepo.Object, default);


            //Act
            var controller = new CatalogItemImagesController(service, repo);
            var actionResult = await controller.ChangeSize(productID, sizeID);

            //Assert
            Assert.NotNull(actionResult);
            var actual = Assert.IsAssignableFrom<NotFoundObjectResult>(actionResult);
            Assert.Equal(expectedMSG, actual.Value.ToString());
        }
    }
}
