using EventBus.MockedTest;
using MarketingService.API.Controllers;
using MarketingService.API.Data;
using MarketingService.API.Services.Repositories.Implementations;
using MarketingService.API.Services.Services.Implementations;
using MarketingService.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MarketingService.Tests.API.Controllers.NewsletterPublisherTests
{
    public class PublishActionTests
    {
        private DbContextOptions<MarketingDbContext> dbOptions;

        public PublishActionTests()
        {
            dbOptions = new DbContextOptionsBuilder<MarketingDbContext>()
                .UseInMemoryDatabase(databaseName: $"db-marketing-test-{System.Reflection.MethodBase.GetCurrentMethod().Name}")
                .Options;

            using var dbContext = new MarketingDbContext(dbOptions);

            try
            {

                if (dbContext.Members.Any() == false)
                {
                    dbContext.AddRange(FakeData.GetMembers());
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
        public async void Publish_ShouldReturnSuccess()
        {
            //Arrange
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new NewsletterRepository(dbContext);
            var subscriberRepo = new SubscriberRepository(dbContext);
            var eventBus = new MagicBus();
            var service = new NewsletterPublisherService(dbContext, repo, eventBus, subscriberRepo);

            var id = 1;

            //Act
            var controller = new NewsletterPublisherController(repo, service);
            var actionResult = await controller.Publish(id);
            var newsletter = await repo.Get(id);

            //Assert
            Assert.NotNull(newsletter);
            Assert.NotNull(newsletter.Sended);
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async void Publish_ShouldReturnBadRequestForIDMinus1()
        {
            //Arrange
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new NewsletterRepository(dbContext);
            var subscriberRepo = new SubscriberRepository(dbContext);
            var eventBus = new MagicBus();
            var service = new NewsletterPublisherService(dbContext, repo, eventBus, subscriberRepo);

            var id = -1;

            //Act
            var controller = new NewsletterPublisherController(repo, service);
            var actionResult = await controller.Publish(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Publish_ShouldReturnNotFoundForID100()
        {
            //Arrange
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new NewsletterRepository(dbContext);
            var subscriberRepo = new SubscriberRepository(dbContext);
            var eventBus = new MagicBus();
            var service = new NewsletterPublisherService(dbContext, repo, eventBus, subscriberRepo);

            var id = 100;

            //Act
            var controller = new NewsletterPublisherController(repo, service);
            var actionResult = await controller.Publish(id);
            var newsletter = await repo.Get(id);

            //Assert
            Assert.Null(newsletter);
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
