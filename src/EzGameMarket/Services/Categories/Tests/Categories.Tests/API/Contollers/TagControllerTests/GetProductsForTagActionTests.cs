using Categories.API.Controllers;
using Categories.API.Exceptions;
using Categories.API.Models;
using Categories.API.Services.Repositories.Implementations;
using Categories.Tests.Fakeimplementations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Categories.Tests.API.Contollers.TagControllerTests
{
    public class GetProductsForTagActionTests
    {
        [Fact]
        public async void GetProductsForTagID_ShouldReturnSuccessForID1And3Products()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            var repo = new TagRepository(dbContext.DbContext);
            var tagID = 1;

            var expectedProductsSize = 3;

            //Act
            var controller = new TagController(repo);
            var actionResult = await controller.GetProductsForTagID(tagID);
            var productsItems = await repo.GetProductsForTagID(tagID);

            //Assert
            Assert.NotNull(productsItems);
            Assert.Equal(expectedProductsSize,productsItems.Count);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<List<string>>>(actionResult);
            var products = Assert.IsAssignableFrom<List<string>>(actionResult.Value);
            Assert.NotNull(products);
            Assert.Equal(expectedProductsSize,products.Count);
            Assert.Equal(productsItems.Count,products.Count);
        }

        [Fact]
        public async void GetProductsForTagID_ShouldReturnBadRequestForIDMinus1()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            var repo = new TagRepository(dbContext.DbContext);
            var tagID = -1;

            //Act
            var controller = new TagController(repo);
            var actionResult = await controller.GetProductsForTagID(tagID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void GetProductsForTagID_ShouldReturnNotFoundID200()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            var repo = new TagRepository(dbContext.DbContext);
            var tagID = 200;

            //Act
            var controller = new TagController(repo);
            var actionResult = await controller.GetProductsForTagID(tagID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
