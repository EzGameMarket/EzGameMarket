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
    public class GetCategoryActionTests
    {
        [Fact]
        public async void GetForID_ShouldReturnSuccessForID1And1Tag()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext();
            var repo = new CategoryRepository(dbContext.DbContext);
            var categoryID = 1;

            //Act
            var controller = new CategoryController(repo);
            var actionResult = await controller.GetCategory(categoryID);
            var tagItem = await repo.GetCategory(categoryID);

            //Assert
            Assert.NotNull(tagItem);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<Category>>(actionResult);
            var tag = Assert.IsAssignableFrom<Category>(actionResult.Value);
            Assert.NotNull(tag);
        }

        [Fact]
        public async void GetForID_ShouldReturnBadRequestForIDMinus1()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext();
            var repo = new CategoryRepository(dbContext.DbContext);
            var categoryID = -1;

            //Act
            var controller = new CategoryController(repo);
            var actionResult = await controller.GetCategory(categoryID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void GetForID_ShouldReturnNotFoundID200()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext();
            var repo = new CategoryRepository(dbContext.DbContext);
            var categoryID = 200;

            //Act
            var controller = new CategoryController(repo);
            var actionResult = await controller.GetCategory(categoryID);
            var tagItem = await repo.GetCategory(categoryID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
            Assert.Null(tagItem);
        }
    }
}
