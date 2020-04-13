using CatalogImages.API.Data;
using CatalogImages.API.Exceptions.Images.Model;
using CatalogImages.API.Models;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Repository.CatalogImage
{
    public class ModifyMethodTests
    {
        [Theory]
        [MemberData(nameof(CreateSuccessTestData))]
        public async void Modify_ShouldCreateNewImage(int id, CatalogItemImageModel model)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{model.ID.GetValueOrDefault()}-{model.ImageUri}-{model.ProductID}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            //Act
            var repo = new CatalogImageRepository(dbContext,default,default);
            await repo.Modify(id, model);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(model.ProductID, actual.ProductID);
            Assert.Equal(model.ImageUri, actual.ImageUri);
        }

        public static object[][] CreateSuccessTestData() => new object[][]
        {
            new object[] {1, new CatalogItemImageModel() { ID = 1, ImageUri = "http://www.cdn.ezgame.hu/catalog/imgs/csgo2.png", ProductID = "gtav", SizeID = 1, TypeID = 1 } },
            new object[] {1, new CatalogItemImageModel() { ID = 1, ImageUri = "test", ProductID = "bfv", SizeID = 1, TypeID = 1 } },
        };

        [Theory]
        [MemberData(nameof(CreateExceptionThrowTestData))]
        public async void Modify_ShouldThrowException(int id, CatalogItemImageModel model, Type expectedExceptionType)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{model.ID.GetValueOrDefault()}-{model.ImageUri}-{expectedExceptionType.FullName}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            //Act
            var repo = new CatalogImageRepository(dbContext,default,default);
            var uploadTask = repo.Modify(id, model);

            //Assert
            await Assert.ThrowsAsync(expectedExceptionType, () => uploadTask);
        }

        public static object[][] CreateExceptionThrowTestData() => new object[][]
        {
            new object[] {1, new CatalogItemImageModel() { ID = 2, ImageUri = "http://www.cdn.ezgame.hu/catalog/imgs/csgo2.png", ProductID = "gtav" }, typeof(ArgumentException) },
            new object[] {1, new CatalogItemImageModel() { ID = default, ImageUri = "http://www.cdn.ezgame.hu/catalog/imgs/csgo2.png", ProductID = "gtav" }, typeof(CImageNotFoundException) },
            new object[] {1, new CatalogItemImageModel() { ID = 100, ImageUri = "http://www.cdn.ezgame.hu/catalog/imgs/csgo2.png", ProductID = "gtav" }, typeof(CImageNotFoundException) },
            new object[] {1, new CatalogItemImageModel() { ID = 1, ImageUri = "http://www.cdn.ezgame.hu/catalog/imgs/csgo2.png", ProductID = "bfv", Size = new ImageSizeModel() { Height = 312, Width = 312, Name = "test" } }, typeof(CImageSizeNotAllowedUpdateException) },
            new object[] {1, new CatalogItemImageModel() { ID = 1, ImageUri = "http://www.cdn.ezgame.hu/catalog/imgs/csgo2.png", ProductID = "bfv", Type = new ImageTypeModel() { Name = "test" } }, typeof(CImageTypeNotAllowedUpdateException) },
        };
    }
}
