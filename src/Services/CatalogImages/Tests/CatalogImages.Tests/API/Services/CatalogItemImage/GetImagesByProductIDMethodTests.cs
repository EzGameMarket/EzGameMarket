using CatalogImages.API.Data;
using CatalogImages.API.Models;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Services.CatalogItemImage
{
    public class GetImagesByProductIDMethodTests
    {
        [Fact]
        public async void GetByID_ShouldReturnSuccess()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            var productID = "csgo";
            var expectedItemsSize = 2;

            //Act
            var repo = new CatalogItemImageService(dbContext, default, default);
            var actual = await repo.GetAllImageForProductID(productID);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedItemsSize,actual.Count);
        }

        [Fact]
        public async void GetByID_ShouldReturnNotFoundForHL2()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            var productID = "hl2";

            //Act
            var repo = new CatalogItemImageService(dbContext, default, default);
            var actual = await repo.GetAllImageForProductID(productID);

            //Assert
            Assert.Empty(actual);
        }
    }
}
