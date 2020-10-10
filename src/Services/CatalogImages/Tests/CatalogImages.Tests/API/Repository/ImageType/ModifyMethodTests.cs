using CatalogImages.API.Data;
using CatalogImages.API.Exceptions.ImageSize.Model;
using CatalogImages.API.Exceptions.ImageType.Model;
using CatalogImages.API.Models;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Repository.ImageType
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
            var model = new ImageTypeModel() { ID = 1, Name = "Catalog2" };

            //Act
            var repo = new ImageTypeRepository(dbContext);
            await repo.Modify(id, model);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(model.Name, actual.Name);
        }

        [Theory]
        [MemberData(nameof(CreateExceptionThrowTestData))]
        public async void Modify_ShouldThrowException(int id, ImageTypeModel model, Type expectedExceptionType)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{model.ID.GetValueOrDefault()}-{model.Name}-{expectedExceptionType.FullName}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            //Act
            var repo = new ImageTypeRepository(dbContext);
            var uploadTask = repo.Modify(id, model);

            //Assert
            await Assert.ThrowsAsync(expectedExceptionType, () => uploadTask);
        }

        public static object[][] CreateExceptionThrowTestData() => new object[][]
        {
            new object[] {1, new ImageTypeModel() { ID = default, Name = "Catalog" }, typeof(ArgumentException) },
            new object[] {100, new ImageTypeModel() { ID = 100, Name = "Catalog" }, typeof(ImageTypeNotFoundByIDException) },
        };
    }
}
