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
        public async void GetByTitle_ShouldReturnSuccessAndOneCampaignWithID1()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new CampaignRepository(dbContext);

            var title = "BLCKFRDY itt van";

            //Arange
            var controller = new CampaignController(repo);
            var actionResult = await controller.GetByTitle(title);
            var item = await repo.GetByCampaignTitle(title);

            //Assert
            Assert.NotNull(item);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<Campaign>>(actionResult);
            var value = Assert.IsAssignableFrom<Campaign>(actionResult.Value);

            Assert.NotNull(value);
            Assert.Equal(item.Title, value.Title);
        }
        [Fact]
        public async void GetByTitle_ShouldReturnBadRequestForIDMinus1()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new CampaignRepository(dbContext);

            var title = string.Empty;

            //Arange
            var controller = new CampaignController(repo);
            var actionResult = await controller.GetByTitle(title);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }
        [Fact]
        public async void GetByTitle_ShouldReturnNotFoundForID6()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new CampaignRepository(dbContext);

            var title = "Hello World!";

            //Arange
            var controller = new CampaignController(repo);
            var actionResult = await controller.GetByTitle(title);
            var item = await repo.GetByCampaignTitle(title);

            //Assert
            Assert.Null(item);
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
