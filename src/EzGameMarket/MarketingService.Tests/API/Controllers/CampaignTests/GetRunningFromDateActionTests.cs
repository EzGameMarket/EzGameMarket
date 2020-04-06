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

namespace MarketingService.Tests.API.Controllers.CampaignTests
{
    public class GetRunningFromDateActionTests
    {
        private DbContextOptions<MarketingDbContext> dbOptions;

        public GetRunningFromDateActionTests()
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
        public async void GetRunning_ShouldReturnSuccessAndOneCampaignWithID1()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new CampaignRepository(dbContext);

            var date = DateTime.Now.AddDays(-1);
            var expectedItemSize = 7;

            //Arange
            var controller = new CampaignController(repo);
            var actionResult = await controller.GetRunningCampaigns(date);
            var item = await repo.GetRunningCampaigns(date);

            //Assert
            Assert.NotNull(item);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<List<Campaign>>>(actionResult);
            var value = Assert.IsAssignableFrom<List<Campaign>>(actionResult.Value);

            Assert.NotNull(value);
            Assert.Equal(expectedItemSize, value.Count);
        }
        [Fact]
        public async void GetRunning_ShouldReturnNotFoundForDatetimeNowPlus5Years()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new CampaignRepository(dbContext);

            var date = DateTime.Now.AddYears(5);

            //Arange
            var controller = new CampaignController(repo);
            var actionResult = await controller.GetRunningCampaigns(date);
            var item = await repo.GetRunningCampaigns(date);

            //Assert
            Assert.Empty(item);
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
