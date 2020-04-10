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
    public class AddImageSizeActionTests
    {
        private ImageSizeModel CreateModel() => new ImageSizeModel()
        {
            ID = default,
            Height = 512,
            Width = 512,
            Name = "Big Image"
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
            var controller = new ImageSizesController(dbContext);
            var actionResult = await controller.PostImageSize(model);


            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
        }

        [Theory]
        [MemberData(nameof(CreateTestData))]
        public async void Upload_ShouldReturnBadRequestForErrors(ImageSizeModel model, string expectedErrorMSG)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{expectedErrorMSG}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            
            //Act
            var controller = new ImageSizesController(dbContext);
            var actionResult = await controller.PostImageSize(model);


            //Assert
            Assert.NotNull(actionResult);
            var actual = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);
            Assert.Equal(expectedErrorMSG, actual.Value.ToString());
        }

        public static object[][] CreateTestData() => new object[][]
        {
            new object[] { new ImageSizeModel() { Height = 512, Width = 512, Name = default }, "A név nem lehet null" },
            new object[] { new ImageSizeModel() { Height = ImageSizeModel.MinHeight-1, Width = 512, Name = "Big Image" }, $"A magasság nem lehet kisebb mint {ImageSizeModel.MinHeight}" },
            new object[] { new ImageSizeModel() { Height = 512, Width = ImageSizeModel.MinWidth -1, Name = "Big Image" }, $"A szélleség nem lehet kisebb mint {ImageSizeModel.MinWidth}" },
            new object[] { new ImageSizeModel() { Height = ImageSizeModel.MaxHeight + 1, Width = 512, Name = "Big Image" }, $"A magasság nem lehet nagyobb mint {ImageSizeModel.MaxHeight}" },
            new object[] { new ImageSizeModel() { Height = 512, Width = ImageSizeModel.MaxWidth + 1, Name = "Big Image" }, $"A szélleség nem lehet kisebb mint {ImageSizeModel.MaxWidth}" },
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
            var controller = new ImageSizesController(dbContext);
            var actionResult = await controller.PostImageSize(model);


            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ConflictResult>(actionResult);
        }
    }
}
