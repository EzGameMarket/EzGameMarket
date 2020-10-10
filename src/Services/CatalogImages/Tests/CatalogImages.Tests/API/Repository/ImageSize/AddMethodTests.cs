using CatalogImages.API.Data;
using CatalogImages.API.Exceptions.ImageSize.Model;
using CatalogImages.API.Models;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.Tests.FakeImplementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Repository.ImageSize
{
    public class AddMethodTests
    {
        private ImageSizeModel CreateModel() => new ImageSizeModel()
        {
            ID = default,
            Name = "Test",
            Height = 312,
            Width = 312,
        };

        [Fact]
        public async void Add_ShouldCreateNewImage()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            var model = CreateModel();
            var expectedID = (await dbContext.ImageSizes.MaxAsync(i => i.ID.GetValueOrDefault())) + 1;

            //Act
            var repo = new ImageSizeRepository(dbContext);
            await repo.Add(model);
            var actual = await repo.GetByID(expectedID);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedID, actual.ID);
            Assert.Equal(model.Name, actual.Name);
        }


        [Theory]
        [MemberData(nameof(CreateTestData))]
        public async void Add_ShouldThrowException(ImageSizeModel model, Type expectedExceptionType)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{model.ID.GetValueOrDefault()}-{model.Name}-{model.Width}-x-{model.Height}-{expectedExceptionType.FullName}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            //Act
            var repo = new ImageSizeRepository(dbContext);
            var uploadTask = repo.Add(model);

            //Assert
            await Assert.ThrowsAsync(expectedExceptionType, () => uploadTask);
        }

        public static object[][] CreateTestData() => new object[][]
        {
            new object[] { new ImageSizeModel() { ID = default, Name = "Catalog", Height = 312, Width = 312 }, typeof(ImageSizeAlreadyExistWithNameException) },
            new object[] { new ImageSizeModel() { ID = 1, Name = "Test231", Height = 312, Width = 312 }, typeof(ImageSizeAlreadyExistWithIDException) },
            new object[] { new ImageSizeModel() { ID = default, Name = "Test", Height = 256, Width = 256 },typeof(ImageSizeAlreadyExistWithDimensionException) },
        };
    }
}
