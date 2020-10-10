using CatalogImages.API.Data;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Services.CatalogItemImage
{
    public class FilterImagesMethodTests
    {
        [Theory]
        [InlineData("hl2",default,default,0)]
        [InlineData("hl2",default,"Catalog",0)]
        [InlineData("csgo",default,default,2)]
        [InlineData("csgo","Catalog",default,1)]
        public async void GetByIDWithFiltering_ShouldReturnSuccess(string productID, string filterName, string sizeName, int expectedCount)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName+$"{productID}-{filterName}-{sizeName}-{expectedCount}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            //Act
            var repo = new CatalogItemImageService(dbContext, default, default);
            var actual = await repo.GetAllImageForProductIDByFiltering(productID, filterName, sizeName);

            //Assert
            Assert.NotNull(actual);

            Assert.Equal(expectedCount, actual.Count);
        }
    }
}
