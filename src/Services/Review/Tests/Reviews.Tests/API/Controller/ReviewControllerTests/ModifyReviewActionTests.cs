using Microsoft.AspNetCore.Mvc;
using Review.API.Controllers;
using Review.API.Models;
using Review.API.Services.Repositories.Implementations;
using Reviews.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Reviews.Tests.API.Controller.ReviewControllerTests
{
    public class ModifyReviewActionTests
    {
        [Fact]
        public async void Modify_ShouldReturnSuccess()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);
            var reviewID = 1;

            var model = new UserReview()
            {
                ID = reviewID,
                Name = "Werdnik Krisztián",
                ProductID = "csgo",
                ProductName = "Counter-Strike: Global Offensive",
                Rate = 5,
                ReviewText = "A lehető legjobb játék a bolygón",
                UserID = "kriszw"
            };

            var controller = new ReviewController(reviewRepository);
            var actionResult = await controller.ModifyReview(reviewID,model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async void Modify_ModelIsNullShouldReturnBadRequest()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);
            var reviewID = 1;

            var controller = new ReviewController(reviewRepository);
            var actionResult = await controller.ModifyReview(reviewID, default);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Modify_ReviewIDMinus1ShouldReturnBadRequest()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);
            var reviewID = -1;

            var model = new UserReview()
            {
                ID = reviewID,
                Name = "Werdnik Krisztián",
                ProductID = "csgo",
                ProductName = "Counter-Strike: Global Offensive",
                Rate = 5,
                ReviewText = "A lehető legjobb játék a bolygón",
                UserID = "kriszw"
            };

            var controller = new ReviewController(reviewRepository);
            var actionResult = await controller.ModifyReview(reviewID, model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Modify_NoReviewTextGivenShouldReturnBadRequest()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);
            var reviewID = 1;

            var model = new UserReview()
            {
                ID = reviewID,
                Name = "Werdnik Krisztián",
                ProductID = "csgo",
                ProductName = "Counter-Strike: Global Offensive",
                Rate = 5,
                ReviewText = default,
                UserID = "kriszw"
            };

            var controller = new ReviewController(reviewRepository);
            controller.ModelState.AddModelError("ReviewText","No review text given");
            var actionResult = await controller.ModifyReview(reviewID, model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Modify_ReviewID10ShouldReturnNotFound()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);
            var reviewID = 10;

            var model = new UserReview()
            {
                ID = reviewID,
                Name = "Werdnik Krisztián",
                ProductID = "csgo",
                ProductName = "Counter-Strike: Global Offensive",
                Rate = 5,
                ReviewText = "A lehető legjobb játék a bolygón",
                UserID = "kriszw"
            };

            var controller = new ReviewController(reviewRepository);
            var actionResult = await controller.ModifyReview(reviewID, model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
