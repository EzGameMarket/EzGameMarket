using Xunit;
using System;
using System.Collections.Generic;
using System.Text;
using Reviews.Tests.FakeImplementations;
using Review.API.Services.Repositories.Implementations;
using Review.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Review.API.Models;

namespace Reviews.Tests.API.Controller.ReviewControllerTests
{
    public class GetReviewActionTests
    {
        [Fact]
        public async void GetReview_ShouldReturnSuccessAnd1Review()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);
            var reviewID = 1;

            //Act
            var controller = new ReviewController(reviewRepository);
            var actionResult = await controller.GetReview(reviewID);

            //Assert
            Assert.NotNull(await reviewRepository.GetReview(reviewID));
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<UserReview>>(actionResult);
            var product = Assert.IsAssignableFrom<UserReview>(actionResult.Value);
            Assert.NotNull(product);
        }

        [Fact]
        public async void GetReview_IDIsMinus1ItShouldGiveBadRequest()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);
            var reviewID = -1;

            //Act
            var controller = new ReviewController(reviewRepository);
            var actionResult = await controller.GetReview(reviewID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void GetReview_IDIs10ItShouldGiveNotFound()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);
            var reviewID = 10;

            //Act
            var controller = new ReviewController(reviewRepository);
            var actionResult = await controller.GetReview(reviewID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
