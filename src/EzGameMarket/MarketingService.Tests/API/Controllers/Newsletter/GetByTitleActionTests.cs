using MarketingService.API.Controllers;
using MarketingService.API.Data;
using MarketingService.API.Models;
using MarketingService.API.Services.Repositories.Implementations;
using MarketingService.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MarketingService.Tests.API.Controllers.Newsletter
{
    public class GetByTitleActionTests
    {
        private DbContextOptions<MarketingDbContext> dbOptions;

        public GetByTitleActionTests()
        {
            dbOptions = new DbContextOptionsBuilder<MarketingDbContext>()
                .UseInMemoryDatabase(databaseName: $"db-marketing-test-{System.Reflection.MethodBase.GetCurrentMethod().Name}")
                .Options;

            using var dbContext = new MarketingDbContext(dbOptions);

            try
            {

                if (dbContext.Members.Any() == false)
                {
                    dbContext.AddRange(FakeData.GetNewsletters());
                    dbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                dbContext.ChangeTracker.AcceptAllChanges();
            }
        }

        [Fact]
        public async void GetByTitle_ShouldReturnSuccessAndOneNewsLetterWithTitleSzulinap()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new NewsletterRepository(dbContext);

            var title = "Szülinap!";

            //Arange
            var controller = new NewsletterController(repo);
            var actionResult = await controller.GetByTitle(title);
            var item = await repo.GetByTitle(title);

            //Assert
            Assert.NotNull(item);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<NewsletterMessage>>(actionResult);
            var value = Assert.IsAssignableFrom<NewsletterMessage>(actionResult.Value);

            Assert.NotNull(value);
            Assert.Equal(item.Title, value.Title);
        }
        [Fact]
        public async void GetByTitle_ShouldReturnBadRequestWithTitleEmptyString()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new NewsletterRepository(dbContext);

            var title = string.Empty;

            //Arange
            var controller = new NewsletterController(repo);
            var actionResult = await controller.GetByTitle(title);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }
        [Fact]
        public async void GetByTitle_ShouldReturnNotFoundForTitlehelloobello()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new NewsletterRepository(dbContext);

            var title = "heloobello";

            //Arange
            var controller = new NewsletterController(repo);
            var actionResult = await controller.GetByTitle(title);
            var item = await repo.GetByTitle(title);

            //Assert
            Assert.Null(item);
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
