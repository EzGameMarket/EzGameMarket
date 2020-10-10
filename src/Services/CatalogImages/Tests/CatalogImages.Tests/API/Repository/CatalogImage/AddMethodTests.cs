using CatalogImages.API.Data;
using CatalogImages.API.Exceptions.Images.Model;
using CatalogImages.API.Models;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.Tests.FakeImplementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Repository.CatalogImage
{
    public class AddMethodTests
    {
        private CatalogItemImageModel CreateModel() => new CatalogItemImageModel()
        {
            ID = default,
            ImageUri = "test",
            ProductID = "gtav",
            SizeID = 1,
            TypeID = 1,
        };

        [Fact]
        public async void Add_ShouldCreateNewImage()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            var model = CreateModel();
            var expectedID = (await dbContext.Images.MaxAsync(i=> i.ID.GetValueOrDefault())) + 1;

            //Act
            var repo = new CatalogImageRepository(dbContext,default,default);
            await repo.Add(model);
            var actual = await repo.GetByID(expectedID);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedID,actual.ID);
            Assert.Equal(model.ProductID, actual.ProductID);
        }


        [Theory]
        [MemberData(nameof(CreateTestData))]
        public async void Add_ShouldThrowException(CatalogItemImageModel model, Type expectedExceptionType)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName+$"{model.ID.GetValueOrDefault()}-{model.ImageUri}-{expectedExceptionType.FullName}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            //Act
            var repo = new CatalogImageRepository(dbContext,default,default);
            var uploadTask = repo.Add(model);

            //Assert
            await Assert.ThrowsAsync(expectedExceptionType, () => uploadTask);
        }

        public static object[][] CreateTestData() => new object[][]
        { 
            new object[] { new CatalogItemImageModel() { ID = default, ImageUri = "http://www.cdn.ezgame.hu/catalog/imgs/csgo2.png", ProductID = "gtav" },typeof(CImageAlreadyExistWithUrlException) },
            new object[] { new CatalogItemImageModel() { ID = 1, ImageUri = "test", ProductID = "gtav" },typeof(CImageAlreadyExistWithIDException) },
        };
    }
}
