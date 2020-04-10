using CatalogImages.API.Controllers;
using CatalogImages.API.Data;
using CatalogImages.API.Models;
using CatalogImages.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Controllers.ImageType
{
    public class AddImageTypeActionTests
    {
        private ImageTypeModel CreateModel() => new ImageTypeModel()
        {
            ID = default,
            Name = "Thumbnail"
        };

        [Fact]
        public async void Upload_ShouldReturnCreated()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            var model = CreateModel();

            //Act
            var controller = new ImageTypesController(dbContext);
            var actionResult = await controller.PostImageType(model);


            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
        }

        [Theory]
        [MemberData(nameof(CreateTestData))]
        public async void Upload_ShouldReturnBadRequestForErrors(ImageTypeModel model, string expectedErrorMSG)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{expectedErrorMSG}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);


            //Act
            var controller = new ImageTypesController(dbContext);
            var actionResult = await controller.PostImageType(model);


            //Assert
            Assert.NotNull(actionResult);
            var actual = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);
            Assert.Equal(expectedErrorMSG, actual.Value.ToString());
        }

        public static object[][] CreateTestData() => new object[][]
        {
            new object[] { new ImageTypeModel() { Name = default }, "A név nem lehet null" },
        };

        [Fact]
        public async void Upload_ShouldReturnConflictForID1()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            var model = CreateModel();
            model.ID = 1;

            //Act
            var controller = new ImageTypesController(dbContext);
            var actionResult = await controller.PostImageType(model);


            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ConflictObjectResult>(actionResult);
        }
    }
}
