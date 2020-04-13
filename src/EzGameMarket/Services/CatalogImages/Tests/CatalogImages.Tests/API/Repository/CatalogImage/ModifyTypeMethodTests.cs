using CatalogImages.API.Data;
using CatalogImages.API.Exceptions.Images.Model;
using CatalogImages.API.Exceptions.ImageSize.Model;
using CatalogImages.API.Exceptions.ImageType.Model;
using CatalogImages.API.Models;
using CatalogImages.API.Services.Repositories.Abstractions;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.Tests.FakeImplementations;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Repository.CatalogImage
{
    public class ModifyTypeMethodTests
    {
        [Fact]
        public async void Modify_ShouldModifyTheType()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var mockedImageSizeRepo = new Mock<IImageTypeRepository>();
            

            var id = 1;
            var typeID = 2;

            mockedImageSizeRepo.Setup(s => s.AnyWithID(typeID)).ReturnsAsync(true);

            //Act
            var repo = new CatalogImageRepository(dbContext, default, mockedImageSizeRepo.Object);
            await repo.ModifyType(id, typeID);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actual);
            Assert.NotNull(actual.Type);
            Assert.Equal(typeID, actual.Type.ID);
        }

        [Theory]
        [MemberData(nameof(CreateExceptionThrowTestData))]
        public async void Modify_ShouldThrowException(int id, ImageTypeModel model, Type expectedExceptionType)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{model.ID}-{model.Name}-{expectedExceptionType.FullName}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var mockedImageSizeRepo = new Mock<IImageTypeRepository>();
            mockedImageSizeRepo.Setup(s => s.AnyWithID(model.ID.GetValueOrDefault())).ReturnsAsync(expectedExceptionType != typeof(ImageTypeNotFoundByIDException));


            //Act
            var repo = new CatalogImageRepository(dbContext, default, mockedImageSizeRepo.Object);
            var uploadTask = repo.ModifyType(id, model.ID.GetValueOrDefault());

            //Assert
            await Assert.ThrowsAsync(expectedExceptionType, () => uploadTask);
        }

        public static object[][] CreateExceptionThrowTestData() => new object[][]
        {
            new object[] {-1, new ImageTypeModel() { ID = 2 }, typeof(CImageNotFoundException) },
            new object[] {1, new ImageTypeModel() { ID = 5 }, typeof(ImageTypeNotFoundByIDException) },
        };
    }
}
