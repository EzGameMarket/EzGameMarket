using CatalogImages.API.Data;
using CatalogImages.API.Exceptions.ImageSize.Model;
using CatalogImages.API.Exceptions.ImageType.Model;
using CatalogImages.API.Models;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.Tests.FakeImplementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Repository.ImageType
{
    public class AddMethodTests
    {
        private ImageTypeModel CreateModel() => new ImageTypeModel()
        {
            ID = default,
            Name = "Test",
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
            var repo = new ImageTypeRepository(dbContext);
            await repo.Add(model);
            var actual = await repo.GetByID(expectedID);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedID, actual.ID);
            Assert.Equal(model.Name, actual.Name);
        }


        [Theory]
        [MemberData(nameof(CreateTestData))]
        public async void Add_ShouldThrowException(ImageTypeModel model, Type expectedExceptionType)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{model.ID.GetValueOrDefault()}-{model.Name}-{expectedExceptionType.FullName}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            //Act
            var repo = new ImageTypeRepository(dbContext);
            var uploadTask = repo.Add(model);

            //Assert
            await Assert.ThrowsAsync(expectedExceptionType, () => uploadTask);
        }

        public static object[][] CreateTestData() => new object[][]
        {
            new object[] { new ImageTypeModel() { ID = default, Name = "Catalog" }, typeof(ImageTypeAlreadyExistsWithNameException) },
            new object[] { new ImageTypeModel() { ID = 1, Name = "Test" }, typeof(ImageTypeAlreadyExistsWithIDException) },
        };
    }
}
