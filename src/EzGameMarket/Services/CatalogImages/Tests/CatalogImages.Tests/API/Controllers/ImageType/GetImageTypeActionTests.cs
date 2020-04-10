using CatalogImages.API.Controllers;
using CatalogImages.API.Data;
using CatalogImages.API.Models;
using CatalogImages.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Controllers.ImageType
{
    public class GetImageTypeActionTests
    {
        [Theory]
        [InlineData(1, typeof(ImageTypeModel))]
        [InlineData(-1, typeof(BadRequestResult))]
        [InlineData(null, typeof(BadRequestResult))]
        [InlineData(4, typeof(NotFoundResult))]
        public async void GetImageTypesByID(int? id, Type expectedType)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{id}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            //Act
            var controller = new ImageTypesController(dbContext);
            var actionResult = await controller.GetImageType(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<ImageTypeModel>>(actionResult);

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
