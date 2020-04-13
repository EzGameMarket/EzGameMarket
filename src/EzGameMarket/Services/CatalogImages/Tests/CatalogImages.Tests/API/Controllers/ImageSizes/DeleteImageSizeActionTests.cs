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
    public class DeleteImageSizeActionTests
    {
        [Theory]
        [InlineData(1, typeof(OkResult))]
        [InlineData(-1, typeof(BadRequestResult))]
        [InlineData(null, typeof(BadRequestResult))]
        [InlineData(100, typeof(NotFoundResult))]
        public async void DeleteImageSizes(int? id, Type expecetedType)
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName+$"{id}");
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);
            var repo = new ImageSizeRepository(dbContext);


            //Act
            var controller = new ImageSizesController(repo);
            var actionResult = await controller.DeleteImageSize(id);


            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType(expecetedType, actionResult);
        }
    }
}
