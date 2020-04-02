using Categories.API.Controllers;
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
    public class GetFromIDActionTests
    {
        [Fact]
        public async void GetForID_ShouldReturnSuccessForID1And1Tag()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext();
            var repo = new TagRepository(dbContext.DbContext);
            var tagID = 1;

            //Act
            var controller = new TagController(repo);
            var actionResult = await controller.GetTag(tagID);
            var tagItem = await repo.GetTag(tagID);

            //Assert
            Assert.NotNull(tagItem);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<Tag>>(actionResult);
            var tag = Assert.IsAssignableFrom<Tag>(actionResult.Value);
            Assert.NotNull(tag);
        }

        [Fact]
        public async void GetForID_ShouldReturnBadRequestForIDMinus1()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext();
            var repo = new TagRepository(dbContext.DbContext);
            var tagID = -1;

            //Act
            var controller = new TagController(repo);
            var actionResult = await controller.GetTag(tagID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void GetForID_ShouldReturnNotFoundID200()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext();
            var repo = new TagRepository(dbContext.DbContext);
            var tagID = 200;

            //Act
            var controller = new TagController(repo);
            var actionResult = await controller.GetTag(tagID);
            var tagItem = await repo.GetTag(tagID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
            Assert.Null(tagItem);
        }
    }
}
