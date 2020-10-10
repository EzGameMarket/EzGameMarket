using Categories.API.Controllers;
using Categories.API.Services.Repositories.Implementations;
using Categories.API.ViewModels;
using Categories.Tests.Fakeimplementations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Categories.Tests.API.Contollers.CategoryControllerTests
{
    public class AddNewProductToCategoryActionTests
    {
        [Fact]
        public async void AddNewProductToTag_ShouldReturnSuccessAndHL2ForFPS()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            var repo = new CategoryRepository(dbContext.DbContext);
            var categoryID = 1;
            var productID = "hl2";
            var model = new NewProductToCategoryViewModel() { ProductID = productID, CategoryID = categoryID };

            //Act
            var controller = new CategoryController(repo);
            var actionResult = await controller.AddNewProductToCategoryAsync(model);
            var products = await repo.GetProductsForCategoryID(categoryID);

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
            var repo = new CategoryRepository(dbContext.DbContext);
            var categoryID = -1;
            var productID = "";
            var model = new NewProductToCategoryViewModel() { ProductID = productID, CategoryID = categoryID };

            //Act
            var controller = new CategoryController(repo);
            var actionResult = await controller.AddNewProductToCategoryAsync(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void AddNewProductToTag_ShouldReturnBadRequestForTagIdIsMinus1()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            var repo = new CategoryRepository(dbContext.DbContext);
            var categoryID = -1;
            var productID = "";
            var model = new NewProductToCategoryViewModel() { ProductID = productID, CategoryID = categoryID };

            //Act
            var controller = new CategoryController(repo);
            var actionResult = await controller.AddNewProductToCategoryAsync(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void AddNewProductToTag_ShouldReturnNotFoundID200()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            var repo = new CategoryRepository(dbContext.DbContext);
            var categoryID = 200;
            var productID = "csgo";
            var model = new NewProductToCategoryViewModel() { ProductID = productID, CategoryID = categoryID };

            //Act
            var controller = new CategoryController(repo);
            var actionResult = await controller.AddNewProductToCategoryAsync(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
