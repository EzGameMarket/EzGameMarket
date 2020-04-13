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
    public class ModifyCategoryActionTests
    {
        private static Category CreateModel()
        {
            var model = new Category()
            {
                ID = default,
                Name = "RPG",
                Products = new List<CategoriesAndProductsLink>()
                {
                    new CategoriesAndProductsLink()
                    {
                        ID = 1,
                        ProductID = "csgo"
                    },
                    new CategoriesAndProductsLink()
                    {
                        ID = 2,
                        ProductID = "bfv"
                    },
                    new CategoriesAndProductsLink()
                    {
                        ID = 3,
                        ProductID = "gtav"
                    },
                }
            };
            return model;
        }

        [Fact]
        public async void UpdateTag_ShouldReturnSuccessAndRPGTagForID1()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            var repo = new CategoryRepository(dbContext.DbContext);
            var tagID = 1;

            var expectedTagName = "RPG";
            var model = CreateModel();
            model.ID = tagID;

            //Act
            var controller = new CategoryController(repo);
            var actionResult = await controller.Update(tagID, model);
            var tag = await repo.GetCategory(tagID);

            //Assert
            Assert.NotNull(tag);
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
            Assert.Equal(expectedTagName, tag.Name);
        }

        [Fact]
        public async void UpdateTag_ShouldReturnBadRequestForTagIDIsMinus1()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            var repo = new CategoryRepository(dbContext.DbContext);
            var tagID = -1;
            var model = CreateModel();
            model.ID = tagID;

            //Act
            var controller = new CategoryController(repo);
            var actionResult = await controller.Update(tagID, model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void UpdateTag_ShouldReturnBadRequestForTagNameIsEmptyString()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            var repo = new CategoryRepository(dbContext.DbContext);
            var tagID = 1;
            var model = CreateModel();
            model.ID = tagID;
            model.Name = string.Empty;

            //Act
            var controller = new CategoryController(repo);
            var actionResult = await controller.Update(tagID, model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void UpdateTag_ShouldReturnBadRequestForModelIsInValid()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            var repo = new CategoryRepository(dbContext.DbContext);
            var tagID = 1;
            var model = CreateModel();
            model.ID = tagID;
            model.Name = default;

            //Act
            var controller = new CategoryController(repo);
            controller.ModelState.AddModelError("Name", "A Tag neve nem lehet null");
            var actionResult = await controller.Update(tagID, model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void UpdateTag_ShouldReturnNotFoundForID200()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            var repo = new CategoryRepository(dbContext.DbContext);
            var tagID = 200;
            var model = CreateModel();
            model.ID = tagID;

            //Act
            var controller = new CategoryController(repo);
            var actionResult = await controller.Update(tagID, model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
