using Categories.API.Controllers;
using Categories.API.Models;
using Categories.API.Services.Repositories.Implementations;
using Categories.Tests.Fakeimplementations;
using Categories.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Categories.Tests.API.Contollers.TagControllerTests
{
    public class AddNewProductToTagActionTests
    {
        [Fact]
        public async void AddNewProductToTag_ShouldReturnSuccessAndHL2ForFPS()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            var repo = new TagRepository(dbContext.DbContext);
            var tagID = 1;
            var productID = "hl2";
            var model = new NewProductToTagViewModel() { ProductID = productID, TagID = tagID };

            //Act
            var controller = new TagController(repo);
            var actionResult = await controller.AddNewProductToTagAsync(model);
            var products = await repo.GetProductsForTagID(tagID);

            //Assert
            Assert.NotNull(products);
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
            Assert.Contains(productID, products);
        }

        [Fact]
        public async void AddNewProductToTag_ShouldReturnBadRequestForProductIDIsStringEmpty()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            var repo = new TagRepository(dbContext.DbContext);
            var tagID = -1;
            var productID = "";
            var model = new NewProductToTagViewModel() { ProductID = productID, TagID = tagID };

            //Act
            var controller = new TagController(repo);
            var actionResult = await controller.AddNewProductToTagAsync(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void AddNewProductToTag_ShouldReturnBadRequestForTagIdIsMinus1()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            var repo = new TagRepository(dbContext.DbContext);
            var tagID = -1;
            var productID = "";
            var model = new NewProductToTagViewModel() { ProductID = productID, TagID = tagID };

            //Act
            var controller = new TagController(repo);
            var actionResult = await controller.AddNewProductToTagAsync(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void AddNewProductToTag_ShouldReturnNotFoundID200()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            var repo = new TagRepository(dbContext.DbContext);
            var tagID = 200;
            var productID = "csgo";
            var model = new NewProductToTagViewModel() { ProductID = productID, TagID = tagID };

            //Act
            var controller = new TagController(repo);
            var actionResult = await controller.AddNewProductToTagAsync(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
