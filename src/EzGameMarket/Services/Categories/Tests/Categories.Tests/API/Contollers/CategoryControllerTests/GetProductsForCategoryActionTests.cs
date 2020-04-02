using Categories.API.Controllers;
using Categories.API.Services.Repositories.Implementations;
using Categories.Tests.Fakeimplementations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Categories.Tests.API.Contollers.CategoryControllerTests
{
    public class GetProductsForCategoryActionTests
    {
        [Fact]
        public async void GetProductsForCategory_ShouldReturnSuccessForID1And3Products()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext();
            var repo = new CategoryRepository(dbContext.DbContext);
            var categoryID = 1;

            var expectedProductsSize = 2;

            //Act
            var controller = new CategoryController(repo);
            var actionResult = await controller.GetProductsForCategoryID(categoryID);
            var productsItems = await repo.GetProductsForCategoryID(categoryID);

            //Assert
            Assert.NotNull(productsItems);
            Assert.Equal(expectedProductsSize, productsItems.Count);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<List<string>>>(actionResult);
            var products = Assert.IsAssignableFrom<List<string>>(actionResult.Value);
            Assert.NotNull(products);
            Assert.Equal(expectedProductsSize, products.Count);
            Assert.Equal(productsItems.Count, products.Count);
        }

        [Fact]
        public async void GetProductsForCategory_ShouldReturnBadRequestForIDMinus1()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext();
            var repo = new CategoryRepository(dbContext.DbContext);
            var categoryID = -1;

            //Act
            var controller = new CategoryController(repo);
            var actionResult = await controller.GetProductsForCategoryID(categoryID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void GetProductsForCategory_ShouldReturnNotFoundID200()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext();
            var repo = new CategoryRepository(dbContext.DbContext);
            var categoryID = 200;

            //Act
            var controller = new CategoryController(repo);
            var actionResult = await controller.GetProductsForCategoryID(categoryID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
