using CatalogImages.API.Data;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Repository.ImageSize
{
    public class AnyWithIDMethodTests
    {
        [Theory]
        [InlineData(5, false)]
        [InlineData(-1, false)]
        [InlineData(1, true)]
        public async void Any_ShouldReturnSuccess(int id, bool expectedValue)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{id}-{expectedValue}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            //Act
            var repo = new ImageSizeRepository(dbContext);
            var actual = await repo.AnyWithID(id);

            //Assert
            Assert.Equal(expectedValue, actual);
        }
    }
}
