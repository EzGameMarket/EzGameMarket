using CatalogImages.API.Controllers;
using CatalogImages.API.Data;
using CatalogImages.API.Models;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Controllers.ImageSizes
{
    public class GetAllImageSizeActionTests
    {
        [Fact]
        public async void GetAll_ShouldReturnSuccessAnd2Sizes()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var repo = new ImageSizeRepository(dbContext);

            var expectedItemsSize = 2;

            //Act
            var controller = new ImageSizesController(repo);
            var actionResult = await controller.GetImageSizes();

            
            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<IEnumerable<ImageSizeModel>>>(actionResult);
            var actual = Assert.IsAssignableFrom<IEnumerable<ImageSizeModel>>(actionResult.Value);
            Assert.NotNull(actual);
            Assert.Equal(expectedItemsSize, actual.Count());
        }
    }
}
