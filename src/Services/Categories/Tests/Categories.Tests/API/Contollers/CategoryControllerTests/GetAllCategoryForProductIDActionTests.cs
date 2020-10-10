using Categories.API.Controllers;
using Categories.API.Models;
using Categories.API.Services.Repositories.Implementations;
using Categories.Tests.Fakeimplementations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Categories.Tests.API.Contollers.CategoryControllerTests
{
    public class GetAllCategoryForProductIDActionTests
    {
        [Fact]
        public async void GetAllForProduct_ShouldReturnSuccessForCsgoAnd2Categories()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            var repo = new CategoryRepository(dbContext.DbContext);
            var productID = "csgo";

            var expectedItemsSize = 2;

            //Act
            var controller = new CategoryController(repo);
            var actionResult = await controller.GetCategoriesForProduct(productID);
            var tagItems = await repo.GetCategoriesForProduct(productID);

            //Assert
            Assert.NotNull(tagItems);
            Assert.Equal(expectedItemsSize, tagItems.Count);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<List<Category>>>(actionResult);
            var tags = Assert.IsAssignableFrom<List<Category>>(actionResult.Value);
            Assert.NotNull(tags);
            Assert.Equal(expectedItemsSize, tags.Count);
            Assert.Equal(tagItems.Count, tags.Count);
        }

        [Fact]
        public async void GetAllForProduct_ShouldReturnBadRequestForEmptyString()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            var repo = new CategoryRepository(dbContext.DbContext);
            var productID = string.Empty;

            //Act
            var controller = new CategoryController(repo);
            var actionResult = await controller.GetCategoriesForProduct(productID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void GetAllForProduct_ShouldReturnNotFoundForHL2()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            var repo = new CategoryRepository(dbContext.DbContext);
            var productID = "hl2";

            //Act
            var controller = new CategoryController(repo);
            var actionResult = await controller.GetCategoriesForProduct(productID);
            var tagItems = await repo.GetCategoriesForProduct(productID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
            Assert.Empty(tagItems);
        }
    }
}
