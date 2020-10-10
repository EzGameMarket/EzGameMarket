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
    public class SubscribeActionTests
    {
        private DbContextOptions<MarketingDbContext> dbOptions;

        public SubscribeActionTests()
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
                    dbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                dbContext.ChangeTracker.AcceptAllChanges();
            }
        }

        [Fact]
        public async void Subscribe_ShouldReturnOkForNewSubscriber()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);
            var service = new SubscribeManagerService(dbContext);

            var email = "newemail@test.com";
            var model = new SubscribeViewModel() { EMail = email };

            //Arrange
            var controller = new NewsletterSubscribeManagerController(repo, service);
            var actionResult = await controller.Subscribe(model);
            var member = await repo.GetByEmail(email);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
            Assert.True(member.Active);
        }

        [Fact]
        public async void Subscribe_ShouldReturnAcceptedForExistingMember()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);
            var service = new SubscribeManagerService(dbContext);

            var email = "testExisting@gmail.com";
            var model = new SubscribeViewModel() { EMail = email };

            //Arrange
            var controller = new NewsletterSubscribeManagerController(repo, service);
            var actionResult = await controller.Subscribe(model);
            var member = await repo.GetByEmail(email);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<AcceptedResult>(actionResult);
            Assert.True(member.Active);
        }

        [Fact]
        public async void Subscribe_ShouldBadRequestForEmptyEmail()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);
            var service = new SubscribeManagerService(dbContext);

            var email = string.Empty;
            var model = new SubscribeViewModel() { EMail = email };

            //Arrange
            var controller = new NewsletterSubscribeManagerController(repo, service);
            var actionResult = await controller.Subscribe(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Subscribe_ShouldBadRequestForInvalidModelState()
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
            var actionResult = await controller.Subscribe(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }
    }
}