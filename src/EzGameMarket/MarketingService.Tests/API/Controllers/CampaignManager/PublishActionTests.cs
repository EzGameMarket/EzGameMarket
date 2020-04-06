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
using EventBus.MockedTest;

namespace MarketingService.Tests.API.Controllers.CampaignManager
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

                if (dbContext.Campaigns.Any() == false)
                {
                    dbContext.AddRange(FakeData.GetCampaigns());
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
            var repo = new CampaignRepository(dbContext);
            var eventBus = new MagicBus();
            var service = new CampaignService(dbContext,repo, eventBus);

            var id = 1;

            //Act
            var controller = new CampaignManagerController(repo, service);
            var actionResult = await controller.Publish(id);
            var campaign = await repo.Get(id);

            //Assert
            Assert.NotNull(campaign);
            Assert.True(campaign.Published);
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async void Publish_ShouldReturnBadRequestForIDMinus1()
        {
            //Arrange
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new CampaignRepository(dbContext);
            var eventBus = new MagicBus();
            var service = new CampaignService(dbContext, repo, eventBus);

            var id = -1;

            //Act
            var controller = new CampaignManagerController(repo, service);
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
            var repo = new CampaignRepository(dbContext);
            var eventBus = new MagicBus();
            var service = new CampaignService(dbContext, repo, eventBus);

            var id = 100;

            //Act
            var controller = new CampaignManagerController(repo, service);
            var actionResult = await controller.Publish(id);
            var campaign = await repo.Get(id);

            //Assert
            Assert.Null(campaign);
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void Publish_ShouldReturnConflictForPublishedCampaignWithID3()
        {
            //Arrange
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new CampaignRepository(dbContext);
            var eventBus = new MagicBus();
            var service = new CampaignService(dbContext, repo, eventBus);

            var id = 3;

            //Act
            var controller = new CampaignManagerController(repo, service);
            var actionResult = await controller.Publish(id);
            var campaign = await repo.Get(id);

            //Assert
            Assert.NotNull(campaign);
            Assert.True(campaign.Published);
            Assert.NotNull(actionResult);
            Assert.IsType<ConflictResult>(actionResult);
        }
    }
}
