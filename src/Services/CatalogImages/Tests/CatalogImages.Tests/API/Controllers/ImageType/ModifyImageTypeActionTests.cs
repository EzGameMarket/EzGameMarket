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

namespace CatalogImages.Tests.API.Controllers.ImageType
{
    public class ModifyImageTypeActionTests
    {
        private ImageTypeModel CreateModel() => new ImageTypeModel()
        {
            ID = 2,
            Name = "ProductInfo"
        };

        [Fact]
        public async void Modify_ShouldReturnCreated()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var repo = new ImageTypeRepository(dbContext);

            var id = 1;
            var model = CreateModel();
            model.ID = id;

            //Act
            var controller = new ImageTypesController(repo);
            var actionResult = await controller.PutImageType(id, model);


            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
        }

        [Theory]
        [MemberData(nameof(CreateTestData))]
        public async void Modify_ShouldReturnBadRequestForErrors(int id, ImageTypeModel model, string expectedErrorMSG)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{expectedErrorMSG}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var repo = new ImageTypeRepository(dbContext);

            //Act
            var controller = new ImageTypesController(repo);
            var actionResult = await controller.PutImageType(id, model);


            //Assert
            Assert.NotNull(actionResult);
            var actual = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);
            Assert.Equal(expectedErrorMSG, actual.Value.ToString());
        }

        public static object[][] CreateTestData() => new object[][]
        {
            new object[] { 1, new ImageTypeModel() { ID = 1, Name = default }, "A név nem lehet null" },
            new object[] { -1, new ImageTypeModel() { ID = 1, Name = "Product" }, "Az ID nem lehet kisebb mint 1" },
            new object[] { 2, new ImageTypeModel() { ID = 1, Name = "Product" }, $"A megadott ID és a Modelnek az IDja nem egyenlő" },
        };

        [Fact]
        public async void Modify_ShouldReturnNotFoundForID100()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var repo = new ImageTypeRepository(dbContext);

            var id = 100;
            var model = CreateModel();
            model.ID = id;

            //Act
            var controller = new ImageTypesController(repo);
            var actionResult = await controller.PutImageType(id, model);


            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
