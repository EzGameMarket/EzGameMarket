using Categories.API.Controllers;
using Categories.API.Models;
using Categories.API.Services.Repositories.Implementations;
using Categories.API.ViewModels;
using Categories.Tests.Fakeimplementations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Categories.Tests.API.Contollers.TagControllerTests
{
    public class AddNewTagActionTests
    {
        private static Tag CreateModel()
        {
            var model = new Tag()
            {
                ID = default,
                Name = "RPG",
                //Products = new List<TagsAndProductsLink>()
            };
            return model;
        }

        [Fact]
        public async void AddNewTag_ShouldReturnSuccessAndRPGTag()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext();
            var repo = new TagRepository(dbContext.DbContext);
            //var tagID = 4;

            var expectedTagName = "RPG";
            var model = CreateModel();

            //Act
            var controller = new TagController(repo);
            var actionResult = await controller.Add(model);
            var tag = await repo.GetTag(model.ID.Value);

            //Assert
            Assert.NotNull(tag);
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
            Assert.Equal(expectedTagName, tag.Name);
        }

        [Fact]
        public async void AddNewTag_ShouldReturnBadRequestForTagNameIsEmptyString()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext();
            var repo = new TagRepository(dbContext.DbContext);
            var model = CreateModel();
            model.Name = string.Empty;

            //Act
            var controller = new TagController(repo);
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void AddNewTag_ShouldReturnConflictForTagIdIs1()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext();
            var repo = new TagRepository(dbContext.DbContext);
            var model = CreateModel();
            model.ID = 1;

            //Act
            var controller = new TagController(repo);
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ConflictResult>(actionResult);
        }

        [Fact]
        public async void AddNewTag_ShouldReturnBadRequestForModelIsInValid()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext();
            var repo = new TagRepository(dbContext.DbContext);
            var model = CreateModel();
            model.Name = default;

            //Act
            var controller = new TagController(repo);
            controller.ModelState.AddModelError("Name","A Tag neve nem lehet null");
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }
    }
}
