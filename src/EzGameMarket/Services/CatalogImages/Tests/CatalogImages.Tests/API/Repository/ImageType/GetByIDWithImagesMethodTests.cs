using CatalogImages.API.Data;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Repository.ImageType
{
    public class GetByIDWithImagesMethodTests
    {
        [Theory]
        [InlineData(1, false)]
        [InlineData(5, true)]
        [InlineData(-1, true)]
        public async void GetImageSizeWithImages_ShouldReturnSuccess(int id, bool isNull)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{id}-{isNull}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            //Act
            var repo = new ImageSizeRepository(dbContext);
            var actual = await repo.GetByIDWithImages(id);

            //Assert
            if (isNull)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                Assert.NotNull(actual.Images);
                Assert.NotEmpty(actual.Images);
            }
        }
    }
}
