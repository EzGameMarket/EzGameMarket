using CatalogImages.API.Data;
using CatalogImages.API.Exceptions.ImageType.Model;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Repository.ImageType
{
    public class DeleteMethodTests
    {
        [Fact]
        public async void Delete_ShouldReturnSuccess()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            var id = 1;

            //Act
            var repo = new ImageTypeRepository(dbContext);
            await repo.Delete(id);
            var data = await repo.GetByID(id);

            //Assert
            Assert.Null(data);
        }

        [Fact]
        public async void Delete_ShouldThrowImageSizeNotFoundException()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            var id = 100;

            //Act
            var repo = new ImageTypeRepository(dbContext);
            var deleteTask = repo.Delete(id);

            //Assert
            await Assert.ThrowsAsync<ImageTypeNotFoundByIDException>(() => deleteTask);
        }
    }
}
