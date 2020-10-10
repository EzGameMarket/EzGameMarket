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

namespace MarketingService.Tests.API.Controllers.CampaignManager
{
    public class CancelActionTests
    {
        private DbContextOptions<MarketingDbContext> dbOptions;

        public CancelActionTests()
        {
            dbOptions = new DbContextOptionsBuilder<MarketingDbContext>()
                .UseInMemoryDatabase(databaseName: $"db-marketing-test-{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}")
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
        public async void Cancel_ShouldReturnSuccess()
        {
            //Arrange
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new CampaignRepository(dbContext);
            var eventBus = new MagicBus();
            var service = new CampaignService(dbContext, repo, eventBus);

            var id = 2;

            //Act
            var controller = new CampaignManagerController(repo, service);
            var actionResult = await controller.Cancel(id);
            var campaign = await repo.Get(id);

            //Assert
            Assert.NotNull(campaign);
            Assert.True(campaign.Published);
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async void Cancel_ShouldReturnNoContentForNotPublishedCampaignYet()
        {
            //Arrange
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new CampaignRepository(dbContext);
            var eventBus = new MagicBus();
            var service = new CampaignService(dbContext, repo, eventBus);

            var id = 21;

            //Act
            var controller = new CampaignManagerController(repo, service);
            var actionResult = await controller.Cancel(id);
            var campaign = await repo.Get(id);

            //Assert
            Assert.NotNull(campaign);
            Assert.False(campaign.Published);
            Assert.False(campaign.Started);
            Assert.False(campaign.Canceled);
            Assert.NotNull(actionResult);
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async void Cancel_ShouldReturnNoContentForNotStartedCampaignYet()
        {
            //Arrange
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new CampaignRepository(dbContext);
            var eventBus = new MagicBus();
            var service = new CampaignService(dbContext, repo, eventBus);

            var id = 23;

            //Act
            var controller = new CampaignManagerController(repo, service);
            var actionResult = await controller.Cancel(id);
            var campaign = await repo.Get(id);

            //Assert
            Assert.NotNull(campaign);
            Assert.True(campaign.Published);
            Assert.False(campaign.Started);
            Assert.False(campaign.Canceled);
            Assert.NotNull(actionResult);
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async void Cancel_ShouldReturnBadRequestForIDMinus1()
        {
            //Arrange
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new CampaignRepository(dbContext);
            var eventBus = new MagicBus();
            var service = new CampaignService(dbContext, repo, eventBus);

            var id = -1;

            //Act
            var controller = new CampaignManagerController(repo, service);
            var actionResult = await controller.Cancel(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Cancel_ShouldReturnNotFoundForID100()
        {
            //Arrange
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new CampaignRepository(dbContext);
            var eventBus = new MagicBus();
            var service = new CampaignService(dbContext, repo, eventBus);

            var id = 100;

            //Act
            var controller = new CampaignManagerController(repo, service);
            var actionResult = await controller.Cancel(id);
            var campaign = await repo.Get(id);

            //Assert
            Assert.Null(campaign);
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void Cancel_ShouldReturnConflictForCanceldCampaignWithID4()
        {
            //Arrange
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new CampaignRepository(dbContext);
            var eventBus = new MagicBus();
            var service = new CampaignService(dbContext, repo, eventBus);

            var id = 4;

            //Act
            var controller = new CampaignManagerController(repo, service);
            var actionResult = await controller.Cancel(id);
            var campaign = await repo.Get(id);

            //Assert
            Assert.NotNull(campaign);
            Assert.True(campaign.Canceled);
            Assert.NotNull(actionResult);
            Assert.IsType<ConflictResult>(actionResult);
        }
    }
}
