using CatalogImages.API.Data;
using CatalogImages.API.Exceptions.Images.Model;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Repository.CatalogImage
{
    public class DeleteMethodTests
    {
        [Fact]
        public async void GetImage_ShouldReturnSuccess()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            var id = 1;

            //Act
            var repo = new CatalogImageRepository(dbContext,default,default);
            await repo.Delete(id);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public async void GetImage_ShouldThrowNotFoundException()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            var id = 100;

            //Act
            var repo = new CatalogImageRepository(dbContext,default,default);
            var deleteTask = repo.Delete(id);

            //Assert
            await Assert.ThrowsAsync<CImageNotFoundException>(() => deleteTask);
        }
    }
}
