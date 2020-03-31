using Microsoft.EntityFrameworkCore;
using Review.API.Data;
using Review.API.Exceptions;
using Review.API.Models;
using Review.API.Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Review.API.Services.Repositories.Implementations
{
    public class ReviewRepository : IReviewRepository
    {
        private ReviewDbContext _dbContext;

        public ReviewRepository(ReviewDbContext context)
        {
            _dbContext = context;
        }

        public Task<UserReview> GetReview(int reviewID) => _dbContext.Reviews.FirstOrDefaultAsync(r=> r.ID == reviewID);

        public Task<List<UserReview>> GetReviewsForProduct(int skip, int take, string productID) => _dbContext.Reviews.Where(r => r.ProductID == productID).Skip(skip).Take(take).ToListAsync();

        public Task<List<UserReview>> GetReviewsForUser(string userID) => _dbContext.Reviews.Where(r => r.UserID == userID).ToListAsync();

        public Task<List<UserReview>> GetReviewsForUserWithSkipAndTake(int skip, int take, string userID) => _dbContext.Reviews.Where(r => r.UserID == userID).Skip(skip).Take(take).ToListAsync();

        public Task<long> GetAllReviewCountForProduct(string productID) => _dbContext.Reviews.LongCountAsync(r => r.ProductID == productID);
        public Task<long> GetAllReviewCountForUser(string userID) => _dbContext.Reviews.LongCountAsync(r => r.UserID == userID);

        public async Task Update(int id, UserReview review)
        {
            var reviewToUpdate = await GetReview(id);

            if (reviewToUpdate == default)
            {
                throw new ReviewNotFoundException() { ReviewID = id };
            }

            _dbContext.Entry(reviewToUpdate).CurrentValues.SetValues(review);

            await _dbContext.SaveChangesAsync();
        }

        public async Task Add(UserReview review)
        {
            if (review.ID.HasValue)
            {
                var reviewToUpdate = await GetReview(review.ID.Value);

                if (reviewToUpdate != default)
                {
                    throw new ReviewAlreadyExistsException() { UserID = review.UserID, ReviewID = review.ID.Value, ProductID = review.ProductID };
                } 
            }

            await _dbContext.Reviews.AddAsync(review);

            await _dbContext.SaveChangesAsync();
        }
    }
}
