using CatalogImages.API.Data;
using CatalogImages.API.Exceptions.Images.Model;
using CatalogImages.API.Exceptions.ImageSize.Model;
using CatalogImages.API.Models;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Repository.ImageSize
{
    public class ModifyMethodtests
    {
        [Fact]
        public async void Modify_ShouldCreateNewImage()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            var id = 1;
            var model = new ImageSizeModel() { ID = 1, Name = "Catalog2", Height = 432, Width = 432 };

            //Act
            var repo = new ImageSizeRepository(dbContext);
            await repo.Modify(id, model);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(model.Name, actual.Name);
            Assert.Equal(model.Width, actual.Width);
            Assert.Equal(model.Height, actual.Height);
        }

        [Theory]
        [MemberData(nameof(CreateExceptionThrowTestData))]
        public async void Modify_ShouldThrowException(int id, ImageSizeModel model, Type expectedExceptionType)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{model.ID.GetValueOrDefault()}-{model.Name}-{model.Height}-{model.Width}-{expectedExceptionType.FullName}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            //Act
            var repo = new ImageSizeRepository(dbContext);
            var uploadTask = repo.Modify(id, model);

            //Assert
            await Assert.ThrowsAsync(expectedExceptionType, () => uploadTask);
        }

        public static object[][] CreateExceptionThrowTestData() => new object[][]
        {
            new object[] {1, new ImageSizeModel() { ID = default, Name = "Catalog", Height = 312, Width = 312 }, typeof(ArgumentException) },
            new object[] {100, new ImageSizeModel() { ID = 100, Name = "Catalog", Height = 312, Width = 312 }, typeof(ImageSizeNotFoundByIDException) },
        };
    }
}
