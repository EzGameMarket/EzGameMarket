using Microsoft.AspNetCore.Mvc;
using Review.API.Controllers;
using Review.API.Models;
using Review.API.Services.Repositories.Implementations;
using Reviews.Tests.FakeImplementations;
using Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Reviews.Tests.API.Controller.ReviewControllerTests
{
    public class GetReviewsForUserActionTests
    {
        [Fact]
        public async void GetReviewsForUser_ShouldReturnSuccessAnd2ReviewForKriszW()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);
            var userID = "kriszw";

            var expectedProductRate = 3;
            var expectedItemsSize = 2;

            //Act
            var controller = new ReviewController(reviewRepository);
            var actionResult = await controller.GetReviewsForUsers(userID);
            var repoProducts = await reviewRepository.GetReviewsForUser(userID);
            var productRate = repoProducts.Average(r=> r.Rate);

            //Assert
            Assert.Equal(expectedItemsSize, repoProducts.Count);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<List<UserReview>>>(actionResult);
            var product = Assert.IsAssignableFrom<List<UserReview>>(actionResult.Value);
            Assert.NotNull(product);
            Assert.Equal(expectedItemsSize,product.Count);
            Assert.Equal(expectedProductRate, productRate);
            var controllerProductRate = product.Average(r=> r.Rate);
            Assert.Equal(expectedProductRate, controllerProductRate);
        }

        [Fact]
        public async void GetReviewsForProducts_ShouldReturnSuccessAnd2ReviewForKriszWAnd1ReviewIn1PageWith2TotalItemAnd2MaxPageSize()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);
            var userID = "kriszw";

            var expectedProductRate = 3;
            var expectedItemsSize = 2;

            //Act
            var controller = new ReviewController(reviewRepository);
            var actionResult = await controller.GetReviewsForUsers(userID);
            var repoProducts = await reviewRepository.GetReviewsForUser(userID);
            var productRate = repoProducts.Average(r => r.Rate);

            //Assert
            Assert.Equal(expectedItemsSize, repoProducts.Count);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<List<UserReview>>>(actionResult);
            var product = Assert.IsAssignableFrom<List<UserReview>>(actionResult.Value);
            Assert.NotNull(product);
            Assert.Equal(expectedItemsSize, product.Count);
            Assert.Equal(expectedProductRate, productRate);
            var controllerProductRate = product.Average(r => r.Rate);
            Assert.Equal(expectedProductRate, controllerProductRate);
        }

        [Fact]
        public async void GetReviewsForProducts_ShouldReturnSuccessAnd1ReviewForSalgad()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);
            var userID = "salgad";

            var expectedProductRate = 5;
            var expectedItemsSize = 1;

            //Act
            var controller = new ReviewController(reviewRepository);
            var actionResult = await controller.GetReviewsForUsers(userID);
            var repoProducts = await reviewRepository.GetReviewsForUser(userID);
            var productRate = repoProducts.Average(r => r.Rate);

            //Assert
            Assert.Equal(expectedItemsSize, repoProducts.Count);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<List<UserReview>>>(actionResult);
            var product = Assert.IsAssignableFrom<List<UserReview>>(actionResult.Value);
            Assert.NotNull(product);
            Assert.Equal(expectedItemsSize, product.Count);
            Assert.Equal(expectedProductRate, productRate);
            var controllerProductRate = product.Average(r => r.Rate);
            Assert.Equal(expectedProductRate, controllerProductRate);
        }

        [Fact]
        public async void GetReviewsForProducts_ProductIDIsEmptyStringItShouldGiveBadRequest()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);
            var userID = string.Empty;

            //Act
            var controller = new ReviewController(reviewRepository);
            var actionResult = await controller.GetReviewsForUsers(userID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<List<UserReview>>>(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void GetReviewsForProducts_ProductIDIsCsabaWItShouldGiveNotFound()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);
            var userID = "csabaw";

            //Act
            var controller = new ReviewController(reviewRepository);
            var actionResult = await controller.GetReviewsForUsers(userID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<List<UserReview>>>(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
