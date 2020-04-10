using CatalogImages.API.Controllers;
using CatalogImages.API.Data;
using CatalogImages.API.Models;
using CatalogImages.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CatalogImages.Tests.API.Controllers.ImageType
{
    public class GetAllImageTypeActionTests
    {
        [Fact]
        public async void GetAll_ShouldReturnSuccessAnd2Sizes()
        {
            //Arrange 
            var dbOptions = FakeCatalogImagesDbContextCreator.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeCatalogImagesDbContextCreator.InitDbContext(dbOptions);
            var dbContext = new CatalogImagesDbContext(dbOptions);

            var expectedItemsSize = 2;

            //Act
            var controller = new ImageTypesController(dbContext);
            var actionResult = await controller.GetImageTypes();


            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<IEnumerable<ImageTypeModel>>>(actionResult);
            var actual = Assert.IsAssignableFrom<IEnumerable<ImageTypeModel>>(actionResult.Value);
            Assert.NotNull(actual);
            Assert.Equal(expectedItemsSize, actual.Count());
        }
    }
}
