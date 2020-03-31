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
        private ReviewDbContext _context;

        public ReviewRepository(ReviewDbContext context)
        {
            _context = context;
        }

        public Task<UserReview> GetReview(int reviewID) => _context.Reviews.FirstOrDefaultAsync(r=> r.ID == reviewID);

        public Task<List<UserReview>> GetReviewsForProduct(int skip, int take, string productID) => _context.Reviews.Where(r => r.ProductID == productID).Skip(skip).Take(take).ToListAsync();

        public Task<List<UserReview>> GetReviewsForUser(string userID) => _context.Reviews.Where(r => r.UserID == userID).ToListAsync();

        public Task<List<UserReview>> GetReviewsForUserWithSkipAndTake(int skip, int take, string userID) => _context.Reviews.Where(r => r.UserID == userID).Skip(skip).Take(take).ToListAsync();

        public Task<long> GetAllReviewCountForProduct(string productID) => _context.Reviews.LongCountAsync(r => r.ProductID == productID);
        public Task<long> GetAllReviewCountForUser(string userID) => _context.Reviews.LongCountAsync(r => r.UserID == userID);

        public async Task Update(int id, UserReview review)
        {
            var reviewToUpdate = await _context.Reviews.FirstOrDefaultAsync(r=> r.ID == id);

            if (reviewToUpdate == default)
            {
                throw new ReviewNotFoundException() { ReviewID = id };
            }

            _context.Entry(reviewToUpdate).CurrentValues.SetValues(review);

            await _context.SaveChangesAsync();
        }

        public async Task Add(UserReview review)
        {
            var reviewToUpdate = await _context.Reviews.FirstOrDefaultAsync(r => r.ID == review.ID);

            if (reviewToUpdate != default)
            {
                throw new ReviewAlreadyExistsException() { UserID = review.UserID, ReviewID = review.ID.Value, ProductID = review.ProductID };
            }

            await _context.Reviews.AddAsync(review);

            await _context.SaveChangesAsync();
        }
    }
}
