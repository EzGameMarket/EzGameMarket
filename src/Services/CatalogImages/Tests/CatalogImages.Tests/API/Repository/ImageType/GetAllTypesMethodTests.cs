using CatalogImages.API.Data;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Repository.ImageType
{
    public class GetAllTypesMethodTests
    {
        [Fact]
        public async void GetImageSize_ShouldReturnSuccess()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            var expectedItemsSize = FakeCatalogImagesDbContextCreator.CreateTypes().Count;

            //Act
            var repo = new ImageTypeRepository(dbContext);
            var actual = await repo.GetAllTypes();

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedItemsSize, actual.Count);
        }
    }
}
