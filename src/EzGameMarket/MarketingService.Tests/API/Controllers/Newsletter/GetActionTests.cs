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
    public class GetActionTests
    {
        private DbContextOptions<MarketingDbContext> dbOptions;

        public GetActionTests()
        {
            dbOptions = new DbContextOptionsBuilder<MarketingDbContext>()
                .UseInMemoryDatabase(databaseName: $"db-marketing-test-{System.Reflection.MethodBase.GetCurrentMethod().Name}")
                .Options;

            using var dbContext = new MarketingDbContext(dbOptions);

            try
            {

                if (dbContext.Newsletters.Any() == false)
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
        public async void Get_ShouldReturnSuccessAndOneNewsLetterWithID1()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new NewsletterRepository(dbContext);

            var id = 1;

            //Arange
            var controller = new NewsletterController(repo);
            var actionResult = await controller.Get(id);
            var item = await repo.Get(id);

            //Assert
            Assert.NotNull(item);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<NewsletterMessage>>(actionResult);
            var value = Assert.IsAssignableFrom<NewsletterMessage>(actionResult.Value);

            Assert.NotNull(value);
            Assert.Equal(item.Title, value.Title);
        }
        [Fact]
        public async void Get_ShouldReturnBadRequestForIDMinus1()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new NewsletterRepository(dbContext);

            var id = -1;

            //Arange
            var controller = new NewsletterController(repo);
            var actionResult = await controller.Get(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }
        [Fact]
        public async void Get_ShouldReturnNotFoundForID6()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new NewsletterRepository(dbContext);

            var id = 6;

            //Arange
            var controller = new NewsletterController(repo);
            var actionResult = await controller.Get(id);
            var item = await repo.Get(id);

            //Assert
            Assert.Null(item);
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
