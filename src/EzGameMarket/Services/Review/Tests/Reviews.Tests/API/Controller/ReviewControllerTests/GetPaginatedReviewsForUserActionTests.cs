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
    public class GetPaginatedReviewsForUserActionTests
    {
        [Fact]
        public async void GetReviewsForUser_ShouldReturnSuccessAnd2ReviewForKriszW()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);
            var userID = "kriszw";

            var pageIndex = 0;
            var pageSize = 5;

            var skip = pageIndex * pageSize;
            var take = pageSize;

            var expectedItemsSize = 2;
            var expectedTotalItemsSize = 2;
            var expectedPageIndex = pageIndex;
            var expectedPageSize = pageSize;
            var expectedProductRate = 3;

            //Act
            var controller = new ReviewController(reviewRepository);
            var actionResult = await controller.GetPaginatedReviewsForUsers(userID, pageIndex, pageSize);
            var repoProducts = await reviewRepository.GetReviewsForUserWithSkipAndTake(skip, take, userID);

            //Assert
            Assert.Equal(expectedItemsSize, repoProducts.Count);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<ReviewPaginationViewModel<UserReview>>>(actionResult);
            var product = Assert.IsAssignableFrom<ReviewPaginationViewModel<UserReview>>(actionResult.Value);
            Assert.NotNull(product);
            Assert.Equal(expectedPageIndex, product.ActualPage);
            Assert.Equal(expectedPageSize, product.ItemsPerPage);
            Assert.Equal(expectedItemsSize, product.Data.Count());
            Assert.Equal(expectedTotalItemsSize, product.TotalItemsCount);
            Assert.Equal(expectedProductRate, product.ProductRate);
        }

        [Fact]
        public async void GetReviewsForProducts_ShouldReturnSuccessAnd2ReviewForKriszWAnd1ReviewIn1PageWith2TotalItemAnd2MaxPageSize()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);
            var userID = "kriszw";

            var pageIndex = 0;
            var pageSize = 1;

            var skip = pageIndex * pageSize;
            var take = pageSize;

            var expectedItemsSize = 1;
            var expectedTotalItemsSize = 2;
            var expectedPageIndex = pageIndex;
            var expectedPageSize = pageSize;
            var expectedProductRate = 1;

            //Act
            var controller = new ReviewController(reviewRepository);
            var actionResult = await controller.GetPaginatedReviewsForUsers(userID, pageIndex, pageSize);
            var repoProducts = await reviewRepository.GetReviewsForUserWithSkipAndTake(skip, take, userID);

            //Assert
            Assert.Equal(expectedItemsSize, repoProducts.Count);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<ReviewPaginationViewModel<UserReview>>>(actionResult);
            var product = Assert.IsAssignableFrom<ReviewPaginationViewModel<UserReview>>(actionResult.Value);
            Assert.NotNull(product);
            Assert.Equal(expectedPageIndex, product.ActualPage);
            Assert.Equal(expectedPageSize, product.ItemsPerPage);
            Assert.Equal(expectedItemsSize, product.Data.Count());
            Assert.Equal(expectedTotalItemsSize, product.TotalItemsCount);
            Assert.Equal(expectedProductRate, product.ProductRate);
        }

        [Fact]
        public async void GetReviewsForProducts_ShouldReturnSuccessAnd1ReviewForSalgad()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);
            var userID = "salgad";

            var pageIndex = 0;
            var pageSize = 5;

            var skip = pageIndex * pageSize;
            var take = pageSize;

            var expectedItemsSize = 1;
            var expectedTotalItemsSize = 1;
            var expectedPageIndex = pageIndex;
            var expectedPageSize = pageSize;
            var expectedProductRate = 5;

            //Act
            var controller = new ReviewController(reviewRepository);
            var actionResult = await controller.GetPaginatedReviewsForUsers(userID, pageIndex, pageSize);
            var repoProducts = await reviewRepository.GetReviewsForUserWithSkipAndTake(skip, take, userID);

            //Assert
            Assert.Equal(expectedItemsSize, repoProducts.Count);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<ReviewPaginationViewModel<UserReview>>>(actionResult);
            var product = Assert.IsAssignableFrom<ReviewPaginationViewModel<UserReview>>(actionResult.Value);
            Assert.NotNull(product);
            Assert.Equal(expectedPageIndex, product.ActualPage);
            Assert.Equal(expectedPageSize, product.ItemsPerPage);
            Assert.Equal(expectedItemsSize, product.Data.Count());
            Assert.Equal(expectedTotalItemsSize, product.TotalItemsCount);
            Assert.Equal(expectedProductRate, product.ProductRate);
        }

        [Fact]
        public async void GetReviewsForProducts_ProductIDIsEmptyStringItShouldGiveBadRequest()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);
            var userID = string.Empty;

            var pageIndex = 0;
            var pageSize = 5;

            //Act
            var controller = new ReviewController(reviewRepository);
            var actionResult = await controller.GetReviewsForProducts(userID, pageIndex, pageSize);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<ReviewPaginationViewModel<UserReview>>>(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void GetReviewsForProducts_ProductIDIsCsabaWItShouldGiveNotFound()
        {
            //Arange
            var dbContext = new FakeReviewDbContext();
            var reviewRepository = new ReviewRepository(dbContext.DbContext);
            var productID = "csabaw";

            var pageIndex = 0;
            var pageSize = 5;

            //Act
            var controller = new ReviewController(reviewRepository);
            var actionResult = await controller.GetReviewsForProducts(productID, pageIndex, pageSize);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<ReviewPaginationViewModel<UserReview>>>(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
