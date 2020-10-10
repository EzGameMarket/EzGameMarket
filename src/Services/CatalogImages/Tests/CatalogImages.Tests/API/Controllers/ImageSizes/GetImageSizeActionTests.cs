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
    public class GetImageSizeActionTests
    {
        [Theory]
        [InlineData(1, typeof(ImageSizeModel))]
        [InlineData(-1, typeof(BadRequestResult))]
        [InlineData(null, typeof(BadRequestResult))]
        [InlineData(4, typeof(NotFoundResult))]
        public async void GetImageSizesByID(int? id, Type expectedType)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName+$"{id}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var repo = new ImageSizeRepository(dbContext);

            //Act
            var controller = new ImageSizesController(repo);
            var actionResult = await controller.GetImageSize(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<ImageSizeModel>>(actionResult);

            if (actionResult.Value != default)
            {
                Assert.IsType(expectedType, actionResult.Value);
            }
            else
            {
                Assert.IsType(expectedType, actionResult.Result);
            }
        }
    }
}
