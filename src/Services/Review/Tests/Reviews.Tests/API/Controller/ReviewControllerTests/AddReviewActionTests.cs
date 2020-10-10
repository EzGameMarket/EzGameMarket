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
    public class AddReviewActionTests
    {
        private UserReview CreateReview() => new UserReview()
        {
                ID = default,
                Name = "Werdnik Krisztián",
                ProductID = "fifa2020",
                ProductName = "Counter-Strike: Global Offensive",
                Rate = 5,
                ReviewText = "A lehető legjobb játék a bolygón",
                UserID = "test"
        };

        [Fact]
        public async void Add_ShouldReturnSuccess()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);

            var model = CreateReview();

            var controller = new ReviewController(reviewRepository);
            var actionResult = await controller.AddReviewToProduct(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async void AddReview_ModelIsNullShouldReturnBadRequest()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);

            var controller = new ReviewController(reviewRepository);
            var actionResult = await controller.AddReviewToProduct(default);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Add_NoReviewTextGivenShouldReturnBadRequest()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);

            var model = CreateReview();
            model.ReviewText = default;

            var controller = new ReviewController(reviewRepository);
            controller.ModelState.AddModelError("ReviewText", "No review text given");
            var actionResult = await controller.AddReviewToProduct(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Add_ReviewID1ShouldReturnConflict()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);

            var model = CreateReview();
            model.ID = 1;

            var controller = new ReviewController(reviewRepository);
            var actionResult = await controller.AddReviewToProduct(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ConflictResult>(actionResult);
        }
    }
}
