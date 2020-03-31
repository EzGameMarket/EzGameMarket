using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Review.API.Exceptions;
using Review.API.Models;
using Review.API.Services.Repositories.Abstractions;
using Shared.Extensions.Pagination;

namespace Review.API.Controllers
{
    //Review beküldése előtt a Rechaptát is ki kell tudnia tölteni, hogy a hamis Reviewkat el kerüljük
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        [Route("{reviewID}")]
        public async Task<ActionResult<UserReview>> GetReview([FromRoute]int reviewID)
        {
            if (reviewID < 1)
            {
                return BadRequest();
            }

            var review = await _reviewRepository.GetReview(reviewID);

            if (review != default)
            {
                return review;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("product/{productID}")]
        [AllowAnonymous]
        public async Task<ActionResult<ReviewPaginationViewModel<UserReview>>> GetReviewsForProducts([FromRoute]string productID = null, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 5)
        {
            if (string.IsNullOrEmpty(productID))
            {
                return BadRequest();
            }

            var skip = pageIndex * pageSize;
            var take = pageSize;

            var items = await _reviewRepository.GetReviewsForProduct(skip,take,productID);

            if (items != default && items.Count > 0)
            {
                var totalItemsCount = await _reviewRepository.GetAllReviewCountForProduct(productID);
                var productRate = Math.Round(items.Average(r => r.Rate), 1);

                return new ReviewPaginationViewModel<UserReview>(totalItemsCount,pageIndex,pageSize,items)
                {
                    ProductRate = productRate
                };
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("user/{userID}")]
        public async Task<ActionResult<ReviewPaginationViewModel<UserReview>>> GetPaginatedReviewsForUsers([FromRoute]string userID = null, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 5)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return BadRequest();
            }

            var skip = pageIndex * pageSize;
            var take = pageSize;

            var items = await _reviewRepository.GetReviewsForUserWithSkipAndTake(skip, take, userID);

            if (items != default && items.Count > 0)
            {
                var totalItemsCount = await _reviewRepository.GetAllReviewCountForUser(userID);
                var productRate = Math.Round(items.Average(r => r.Rate), 1);

                return new ReviewPaginationViewModel<UserReview>(totalItemsCount, pageIndex, pageSize, items)
                {
                    ProductRate = productRate
                };
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("user/{userID}")]
        public async Task<ActionResult<List<UserReview>>> GetReviewsForUsers([FromRoute]string userID = null)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return BadRequest();
            }

            var items = await _reviewRepository.GetReviewsForUser(userID);

            if (items != default && items.Count > 0)
            {
                return items;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddReviewToProduct([FromBody]UserReview review)
        {
            if (review == default || ModelState.IsValid == false)
            {
                return BadRequest();
            }

            try
            {
                await _reviewRepository.Add(review);
                return Ok();
            }
            catch (ReviewAlreadyExistsException)
            {
                return Conflict();
            }
        }
        [HttpPost]
        [Route("modify")]
        public async Task<ActionResult> ModifyReview([FromBody] int reviewID,[FromBody]UserReview review)
        {
            if (reviewID <= 0 || ModelState.IsValid == false || review == default)
            {
                return BadRequest();
            }

            try
            {
                await _reviewRepository.Update(reviewID, review);

                return Ok();
            }
            catch (ReviewNotFoundException)
            {
                return NotFound();
            }
        }
    }
}