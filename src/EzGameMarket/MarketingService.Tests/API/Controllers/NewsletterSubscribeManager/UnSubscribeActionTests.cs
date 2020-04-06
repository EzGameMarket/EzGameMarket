using MarketingService.API.Controllers;
using MarketingService.API.Data;
using MarketingService.API.Services.Repositories.Implementations;
using MarketingService.API.Services.Services.Implementations;
using MarketingService.API.ViewModels.Subscribe;
using MarketingService.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MarketingService.Tests.API.Controllers.NewsletterSubscribeManager
{
    public class UnSubscribeActionTests
    {
        private DbContextOptions<MarketingDbContext> dbOptions;

        public UnSubscribeActionTests()
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
                    dbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                dbContext.ChangeTracker.AcceptAllChanges();
            }
        }

        [Fact]
        public async void UnSubscribe_ShouldReturnAcceptedForExistingMember()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);
            var service = new SubscribeManagerService(dbContext);

            var email = "unSubtestExisting@gmail.com";
            var model = new SubscribeViewModel() { EMail = email };

            //Arrange
            var controller = new NewsletterSubscribeManagerController(repo, service);
            var actionResult = await controller.UnSubscribe(model);
            var member = await repo.GetByEmail(email);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<AcceptedResult>(actionResult);
            Assert.False(member.Active);
        }

        [Fact]
        public async void UnSubscribe_ShouldReturnNotFoundForNewSubscriber()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);
            var service = new SubscribeManagerService(dbContext);

            var email = "newemail@test.com";
            var model = new SubscribeViewModel() { EMail = email };

            //Arrange
            var controller = new NewsletterSubscribeManagerController(repo, service);
            var actionResult = await controller.UnSubscribe(model);
            var member = await repo.GetByEmail(email);

            //Assert
            Assert.Null(member);
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void UnSubscribe_ShouldBadRequestForEmptyEmail()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);
            var service = new SubscribeManagerService(dbContext);

            var email = string.Empty;
            var model = new SubscribeViewModel() { EMail = email };

            //Arrange
            var controller = new NewsletterSubscribeManagerController(repo, service);
            var actionResult = await controller.UnSubscribe(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void UnSubscribe_ShouldBadRequestForInvalidModelState()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);
            var service = new SubscribeManagerService(dbContext);

            var email = string.Empty;
            var model = new SubscribeViewModel() { EMail = email };

            //Arrange
            var controller = new NewsletterSubscribeManagerController(repo, service);
            controller.ModelState.AddModelError("Email", "Az email nem lehet üres string");
            var actionResult = await controller.UnSubscribe(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }
    }
}
