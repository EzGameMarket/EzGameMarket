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
    public class AddActionTests
    {
        private DbContextOptions<MarketingDbContext> dbOptions;

        public AddActionTests()
        {
            dbOptions = new DbContextOptionsBuilder<MarketingDbContext>()
                .UseInMemoryDatabase(databaseName: $"db-marketing-test-{System.Reflection.MethodBase.GetCurrentMethod().Name}")
                .Options;

            using var dbContext = new MarketingDbContext(dbOptions);

            try
            {

                if (dbContext.Members.Any() == false)
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

        private Campaign CreateModel() => new Campaign()
        {
            ID = default,
            CampaignImage = "test.png",
            CouponCode = "TST",
            Description = "Ez egy test tehát: Hello World!",
            End = DateTime.Now,
            Start = DateTime.Now,
            ShortDescription = "Test",
            Title = "Test"
        };

        [Fact]
        public async void Add_ShouldReturnSuccessAndOneCampaign()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new CampaignRepository(dbContext);

            var model = CreateModel();
            var id = await dbContext.Campaigns.CountAsync() + 1;

            //Arange
            var controller = new CampaignController(repo);
            var actionResult = await controller.Add(model);
            var item = await repo.Get(id);

            //Assert
            Assert.NotNull(item);
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
            Assert.Equal(item.Title, model.Title);
        }
        [Fact]
        public async void Add_ShouldReturnBadRequestForTitleIsEmptyString()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new CampaignRepository(dbContext);

            var model = CreateModel();
            model.Title = string.Empty;

            //Arange
            var controller = new CampaignController(repo);
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Add_ShouldReturnBadRequestForInvalidTitle()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new CampaignRepository(dbContext);

            var model = CreateModel();
            model.Title = default;

            //Arange
            var controller = new CampaignController(repo);
            controller.ModelState.AddModelError("Title", "A cím nem lehet null");
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }
        [Fact]
        public async void Add_ShouldReturnConflictForID1()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new CampaignRepository(dbContext);

            var id = 1;
            var model = CreateModel();
            model.ID = id;

            //Arange
            var controller = new CampaignController(repo);
            var actionResult = await controller.Add(model);
            var item = await repo.Get(id);

            //Assert
            Assert.NotNull(item);
            Assert.NotNull(actionResult);
            Assert.IsType<ConflictResult>(actionResult);
        }

        [Fact]
        public async void Add_ShouldReturnConflictForSameTitle()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new CampaignRepository(dbContext);

            var id = 1;
            var model = CreateModel();
            model.Title = "BLCKFRDY itt van";

            //Arange
            var controller = new CampaignController(repo);
            var actionResult = await controller.Add(model);
            var item = await repo.Get(id);

            //Assert
            Assert.NotNull(item);
            Assert.NotNull(actionResult);
            Assert.IsType<ConflictResult>(actionResult);
        }
    }
}
