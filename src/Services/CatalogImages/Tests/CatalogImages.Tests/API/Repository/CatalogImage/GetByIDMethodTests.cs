using CatalogImages.API.Data;
using CatalogImages.API.Models;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Repository.CatalogImage
{
    public class GetByIDMethodTests
    {
        [Theory]
        [InlineData(1, false)]
        [InlineData(-1, true)]
        public async void GetImage_ShouldReturnSuccess(int id, bool isNull)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName+$"{id}-{isNull}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            //Act
            var repo = new CatalogImageRepository(dbContext,default,default);
            var actual = await repo.GetByID(id);

            //Assert
            if (isNull == false)
            {
                Assert.NotNull(actual);
            }
            else
            {
                Assert.Null(actual);
            }
        }
    }
}
