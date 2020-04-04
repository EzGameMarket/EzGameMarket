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

namespace MarketingService.Tests.API.Controllers.Subscribers
{
    public class GetActiveActionTests
    {
        private DbContextOptions<MarketingDbContext> dbOptions;

        public GetActiveActionTests()
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
        public async void GetActives_ShouldReturnSuccessAnd3Members()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);

            var expectedItemsSize = 3;

            //Arange
            var controller = new SubscribersController(repo);
            var actionResult = await controller.GetActiveSubscribers();
            var item = await repo.GetActiveMembers();

            //Assert
            Assert.NotNull(item);
            Assert.Equal(expectedItemsSize,item.Count);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<List<SubscribedMember>>>(actionResult);
            var value = Assert.IsAssignableFrom<List<SubscribedMember>>(actionResult.Value);

            Assert.NotNull(value);
            Assert.Equal(expectedItemsSize, value.Count);
        }
    }
}
