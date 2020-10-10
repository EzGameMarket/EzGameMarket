using CatalogImages.API.Data;
using CatalogImages.API.Exceptions.Images.Model;
using CatalogImages.API.Exceptions.ImageSize.Model;
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
    public class ModifySizeMethodTests
    {
        [Fact]
        public async void Modify_ShouldModifyImage()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var mockedImageSizeRepo = new Mock<IImageSizeRepository>();


            var id = 1;
            var sizeID = 2;

            mockedImageSizeRepo.Setup(s => s.AnyWithID(sizeID)).ReturnsAsync(true);

            //Act
            var repo = new CatalogImageRepository(dbContext,mockedImageSizeRepo.Object,default);
            await repo.ModifySize(id, sizeID);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actual);
            Assert.NotNull(actual.Size);
            Assert.Equal(sizeID, actual.Size.ID);
        }

        [Theory]
        [MemberData(nameof(CreateExceptionThrowTestData))]
        public async void Modify_ShouldThrowException(int id, ImageSizeModel model, Type expectedExceptionType)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{model.ID}-{model.Name}-{expectedExceptionType.FullName}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var mockedImageSizeRepo = new Mock<IImageSizeRepository>();
            mockedImageSizeRepo.Setup(s => s.AnyWithID(model.ID.GetValueOrDefault())).ReturnsAsync(expectedExceptionType != typeof(ImageSizeNotFoundByIDException));


            //Act
            var repo = new CatalogImageRepository(dbContext, mockedImageSizeRepo.Object, default);
            var uploadTask = repo.ModifySize(id, model.ID.GetValueOrDefault());

            //Assert
            await Assert.ThrowsAsync(expectedExceptionType, () => uploadTask);
        }

        public static object[][] CreateExceptionThrowTestData() => new object[][]
        {
            new object[] {-1, new ImageSizeModel() { ID = 2 }, typeof(CImageNotFoundException) },
            new object[] {1, new ImageSizeModel() { ID = 5 }, typeof(ImageSizeNotFoundByIDException) },
        };
    }
}
