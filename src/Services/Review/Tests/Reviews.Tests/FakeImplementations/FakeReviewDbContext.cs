using Microsoft.EntityFrameworkCore;
using Review.API.Data;
using Review.API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reviews.Tests.FakeImplementations
{
    public class FakeReviewDbContext
    {
        public ReviewDbContext DbContext { get; set; }
        public DbContextOptions<ReviewDbContext> DbOptions { get; set; }

        public FakeReviewDbContext()
        {
            var _reviews = new List<UserReview>() 
            {
                new UserReview()
                {
                    ID = 1,
                    Name = "Werdnik Krisztián",
                    ProductID = "csgo",
                    ProductName = "Counter-Strike: Global Offensive",
                    Rate = 1,
                    ReviewText = "Egy fos szar csaló gecik",
                    UserID = "kriszw"
                },
                new UserReview()
                {
                    ID = 2,
                    Name = "Salga Dominik",
                    ProductID = "csgo",
                    ProductName = "Counter-Strike: Global Offensive",
                    Rate = 5,
                    ReviewText = "Legjobb játék",
                    UserID = "salgad"
                },
                new UserReview()
                {
                    ID = 3,
                    Name = "Werdnik Krisztián",
                    ProductID = "bfvde",
                    ProductName = "Battlefield V Deluxe Edition",
                    Rate = 5,
                    ReviewText = "Ez game, ez life",
                    UserID = "kriszw"
                },
            };

            DbOptions = new DbContextOptionsBuilder<ReviewDbContext>()
                .UseInMemoryDatabase(databaseName: $"in-memory-review-test").EnableSensitiveDataLogging()
                .Options;

            try
            {
                DbContext = new ReviewDbContext(DbOptions);
                DbContext.AddRange(_reviews);
                DbContext.SaveChanges();
            }
            catch (Exception)
            {
                DbContext.ChangeTracker.AcceptAllChanges();
            }
        }


    }
}