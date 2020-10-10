using Review.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Review.API.Services.Repositories.Abstractions
{
    public interface IReviewRepository
    {
        Task<UserReview> GetReview(int reviewID);

        Task<long> GetAllReviewCountForProduct(string productID);
        Task<long> GetAllReviewCountForUser(string userID);
        Task<List<UserReview>> GetReviewsForProduct(int skip, int take, string productID);

        Task Update(int id,UserReview review);

        Task Add(UserReview review);

        Task<List<UserReview>> GetReviewsForUser(string userID);
        Task<List<UserReview>> GetReviewsForUserWithSkipAndTake(int skip, int take, string userID);
    }
}
