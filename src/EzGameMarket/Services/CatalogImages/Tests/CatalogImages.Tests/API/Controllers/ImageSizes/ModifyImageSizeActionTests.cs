using CatalogImages.API.Controllers;
using CatalogImages.API.Data;
using CatalogImages.API.Models;
using CatalogImages.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Controllers.ImageSizes
{
    public class ModifyImageSizeActionTests
    {
        private ImageSizeModel CreateModel() => new ImageSizeModel()
        {
            ID = default,
            Height = 624,
            Width = 624,
            Name = "Bigger Image"
        };

        [Fact]
        public async void Modify_ShouldReturnCreated()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            var id = 1;
            var model = CreateModel();
            model.ID = id;

            //Act
            var controller = new ImageSizesController(dbContext);
            var actionResult = await controller.PutImageSize(id, model);


            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
        }

        [Theory]
        [MemberData(nameof(CreateTestData))]
        public async void Modify_ShouldReturnBadRequestForErrors(int id, ImageSizeModel model, string expectedErrorMSG)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{expectedErrorMSG}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            //Act
            var controller = new ImageSizesController(dbContext);
            var actionResult = await controller.PutImageSize(id, model);


            //Assert
            Assert.NotNull(actionResult);
            var actual = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);
            Assert.Equal(expectedErrorMSG, actual.Value.ToString());
        }

        public static object[][] CreateTestData() => new object[][]
        {
            new object[] { 1, new ImageSizeModel() { ID = 1, Height = 512, Width = 512, Name = default }, "A név nem lehet null" },
            new object[] { 1, new ImageSizeModel() { ID = 1, Height = ImageSizeModel.MinHeight-1, Width = 512, Name = "Big Image" }, $"A magasság nem lehet kisebb mint {ImageSizeModel.MinHeight}" },
            new object[] { 1, new ImageSizeModel() { ID = 1, Height = 512, Width = ImageSizeModel.MinWidth -1, Name = "Big Image" }, $"A szélleség nem lehet kisebb mint {ImageSizeModel.MinWidth}" },
            new object[] { 1, new ImageSizeModel() { ID = 1, Height = ImageSizeModel.MaxHeight + 1, Width = 512, Name = "Big Image" }, $"A magasság nem lehet nagyobb mint {ImageSizeModel.MaxHeight}" },
            new object[] { 1, new ImageSizeModel() { ID = 1, Height = 512, Width = ImageSizeModel.MaxWidth + 1, Name = "Big Image" }, $"A szélleség nem lehet kisebb mint {ImageSizeModel.MaxWidth}" },
            new object[] { 2, new ImageSizeModel() { ID = 1, Height = 512, Width = ImageSizeModel.MaxWidth + 1, Name = "Big Image" }, $"A megadott ID és a Modelnek az IDja nem egyenlő" },
        };

        [Fact]
        public async void Modify_ShouldReturnNotFoundForID100()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            var id = 100;
            var model = CreateModel();
            model.ID = id;

            //Act
            var controller = new ImageSizesController(dbContext);
            var actionResult = await controller.PutImageSize(id, model);


            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
