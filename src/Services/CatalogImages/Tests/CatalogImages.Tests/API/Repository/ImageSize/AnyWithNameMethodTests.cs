using CatalogImages.API.Data;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Repository.ImageSize
{
    public class AnyWithNameMethodTests
    {
        [Theory]
        [InlineData("test", false)]
        [InlineData("", false)]
        [InlineData("Catalog", true)]
        public async void Any_ShouldReturnSuccess(string name, bool expectedValue)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{name}-{expectedValue}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            //Act
            var repo = new ImageSizeRepository(dbContext);
            var actual = await repo.AnyWithName(name);

            //Assert
            Assert.Equal(expectedValue, actual);
        }
    }
}
