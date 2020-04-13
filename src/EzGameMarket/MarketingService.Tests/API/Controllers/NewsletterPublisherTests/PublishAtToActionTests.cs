using EventBus.MockedTest;
using MarketingService.API.Controllers;
using MarketingService.API.Data;
using MarketingService.API.Services.Repositories.Implementations;
using MarketingService.API.Services.Services.Implementations;
using MarketingService.API.ViewModels.NewsletterPublish;
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
    public class PublishAtToActionTests
    {
        private DbContextOptions<MarketingDbContext> dbOptions;

        public PublishAtToActionTests()
        {
            dbOptions = new DbContextOptionsBuilder<MarketingDbContext>()
                .UseInMemoryDatabase(databaseName: $"db-marketing-test-{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}")
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
        public async void PublishAtTo_ShouldReturnSuccess()
        {
            //Arrange
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new NewsletterRepository(dbContext);
            var subscriberRepo = new SubscriberRepository(dbContext);
            var eventBus = new MagicBus();
            var service = new NewsletterPublisherService(dbContext, repo, eventBus, subscriberRepo);

            var id = 1;
            var time = DateTime.Now.AddDays(2);
            var email = new List<string>()
            {
                "werdnikkrisz@gmail.com"
            };
            var model = new PublishToEmailsAtSpecifiedTimeViewModel(email, time, id);

            //Act
            var controller = new NewsletterPublisherController(repo, service);
            var actionResult = await controller.PublishAtTo(model);
            var newsletter = await repo.Get(id);

            //Assert
            Assert.NotNull(newsletter);
            Assert.NotNull(newsletter.Sended);
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async void PublishAtTo_ShouldReturnBadRequestForIDMinus1()
        {
            //Arrange
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new NewsletterRepository(dbContext);
            var subscriberRepo = new SubscriberRepository(dbContext);
            var eventBus = new MagicBus();
            var service = new NewsletterPublisherService(dbContext, repo, eventBus, subscriberRepo);

            var id = -1;
            var time = DateTime.Now.AddDays(2);
            var email = new List<string>()
            {
                "werdnikkrisz@gmail.com"
            };
            var model = new PublishToEmailsAtSpecifiedTimeViewModel(email, time, id);

            //Act
            var controller = new NewsletterPublisherController(repo, service);
            var actionResult = await controller.PublishAtTo(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void PublishAtTo_ShouldReturnBadRequestForInvalidEmails()
        {
            //Arrange
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new NewsletterRepository(dbContext);
            var subscriberRepo = new SubscriberRepository(dbContext);
            var eventBus = new MagicBus();
            var service = new NewsletterPublisherService(dbContext, repo, eventBus, subscriberRepo);

            var id = 1;
            var email = new List<string>()
            {
                "hello world"
            };
            var time = DateTime.Now.AddDays(2);
            var model = new PublishToEmailsAtSpecifiedTimeViewModel(email, time, id);

            var expectedNumberOfInvalidEmails = email.Count;

            //Act
            var controller = new NewsletterPublisherController(repo, service);
            var actionResult = await controller.PublishAtTo(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestObjectResult>(actionResult);
            var invalidEmails = Assert.IsAssignableFrom<IEnumerable<string>>((actionResult as BadRequestObjectResult).Value);
            Assert.Equal(expectedNumberOfInvalidEmails, invalidEmails.Count());
        }

        [Fact]
        public async void PublishAtTo_ShouldReturnNoContentForPublishingDateIsYesterday()
        {
            //Arrange
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new NewsletterRepository(dbContext);
            var subscriberRepo = new SubscriberRepository(dbContext);
            var eventBus = new MagicBus();
            var service = new NewsletterPublisherService(dbContext, repo, eventBus, subscriberRepo);

            var id = 1;
            var time = DateTime.Now.AddDays(-1);
            var email = new List<string>()
            {
                "werdnikkrisz@gmail.com"
            };
            var model = new PublishToEmailsAtSpecifiedTimeViewModel(email, time, id);

            //Act
            var controller = new NewsletterPublisherController(repo, service);
            var actionResult = await controller.PublishAtTo(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async void PublishAtTo_ShouldReturnNotFoundForID100()
        {
            //Arrange
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new NewsletterRepository(dbContext);
            var subscriberRepo = new SubscriberRepository(dbContext);
            var eventBus = new MagicBus();
            var service = new NewsletterPublisherService(dbContext, repo, eventBus, subscriberRepo);

            var id = 100;
            var time = DateTime.Now.AddDays(2);
            var email = new List<string>()
            {
                "werdnikkrisz@gmail.com"
            };
            var model = new PublishToEmailsAtSpecifiedTimeViewModel(email, time, id);

            //Act
            var controller = new NewsletterPublisherController(repo, service);
            var actionResult = await controller.PublishAtTo(model);
            var newsletter = await repo.Get(id);

            //Assert
            Assert.Null(newsletter);
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
