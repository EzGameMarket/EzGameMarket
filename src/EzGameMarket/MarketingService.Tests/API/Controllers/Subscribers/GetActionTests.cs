using MarketingService.Tests.FakeImplementations;
using MarketingService.API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MarketingService.API.Services.Repositories.Implementations;
using MarketingService.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using MarketingService.API.Models;
using System.Linq;

namespace MarketingService.Tests.API.Controllers.Subscribers
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
        public async void Get_ShouldReturnSuccessAndOneSubscriberWithID1()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);

            var id = 1;

            //Arange
            var controller = new SubscribersController(repo);
            var actionResult = await controller.GetByID(id);
            var item = await repo.Get(id);

            //Assert
            Assert.NotNull(item);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<SubscribedMember>>(actionResult);
            var value = Assert.IsAssignableFrom<SubscribedMember>(actionResult.Value);

            Assert.NotNull(value);
            Assert.Equal(item.EMail, value.EMail);
        }
        [Fact]
        public async void Get_ShouldReturnBadRequestForIDMinus1()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);

            var id = -1;

            //Arange
            var controller = new SubscribersController(repo);
            var actionResult = await controller.GetByID(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }
        [Fact]
        public async void Get_ShouldReturnNotFoundForID100()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);

            var id = 100;

            //Arange
            var controller = new SubscribersController(repo);
            var actionResult = await controller.GetByID(id);
            var item = await repo.Get(id);

            //Assert
            Assert.Null(item);
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
