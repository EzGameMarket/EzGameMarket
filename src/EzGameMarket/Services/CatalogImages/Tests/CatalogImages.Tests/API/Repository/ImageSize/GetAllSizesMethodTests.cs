using CatalogImages.API.Data;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Repository.ImageSize
{
    public class GetAllSizesMethodTests
    {
        [Fact]
        public async void GetImageSize_ShouldReturnSuccess()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            var expectedItemsSize = FakeCatalogImagesDbContextCreator.CreateSizes().Count;

            //Act
            var repo = new ImageSizeRepository(dbContext);
            var actual = await repo.GetAllSizes();

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedItemsSize, actual.Count);
        }
    }
}
