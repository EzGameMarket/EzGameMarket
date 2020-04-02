using Categories.API.Controllers;
using Categories.API.Services.Repositories.Implementations;
using Categories.Tests.Fakeimplementations;
using Categories.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Categories.Tests.API.Contollers.TagControllerTests
{
    public class GetAllForProductActionTests
    {
        [Fact]
        public async void GetAllForProduct_ShouldReturnSuccessForCsgoAnd2Tags()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext();
            var repo = new TagRepository(dbContext.DbContext);
            var productID = "csgo";

            var expectedItemsSize = 2;

            //Act
            var controller = new TagController(repo);
            var actionResult = await controller.GetTagsForProduct(productID);
            var tagItems = await repo.GetTagsForProduct(productID);

            //Assert
            Assert.NotNull(tagItems);
            Assert.Equal(expectedItemsSize,tagItems.Count);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<List<Tag>>>(actionResult);
            var tags = Assert.IsAssignableFrom<List<Tag>>(actionResult.Value);
            Assert.NotNull(tags);
            Assert.Equal(expectedItemsSize,tags.Count);
            Assert.Equal(tagItems.Count, tags.Count);
        }

        [Fact]
        public async void GetAllForProduct_ShouldReturnBadRequestForEmptyString()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext();
            var repo = new TagRepository(dbContext.DbContext);
            var productID = string.Empty;

            //Act
            var controller = new TagController(repo);
            var actionResult = await controller.GetTagsForProduct(productID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void GetAllForProduct_ShouldReturnNotFoundForHL2()
        {
            //Arange
            var dbContext = new FakeCategoryDbContext();
            var repo = new TagRepository(dbContext.DbContext);
            var productID = "hl2";

            //Act
            var controller = new TagController(repo);
            var actionResult = await controller.GetTagsForProduct(productID);
            var tagItems = await repo.GetTagsForProduct(productID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
            Assert.Empty(tagItems);
        }
    }
}
