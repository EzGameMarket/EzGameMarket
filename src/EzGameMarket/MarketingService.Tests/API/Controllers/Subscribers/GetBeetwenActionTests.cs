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
    public class GetBeetwenActionTests
    {
        private DbContextOptions<MarketingDbContext> dbOptions;

        public GetBeetwenActionTests()
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


        [Theory, MemberData(nameof(SuccessData))]
        public async void GetBeetwen_ShouldReturnSucces(DateTime start, DateTime end, bool active, int expectedDataSize)
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);

            //Arange
            var controller = new SubscribersController(repo);
            var actionResult = await controller.GetBeetwen(start, end,active);
            var item = await repo.GetBeetwen(start,end,active);

            //Assert
            Assert.NotNull(item);
            Assert.Equal(expectedDataSize,item.Count);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<List<SubscribedMember>>>(actionResult);
            if (expectedDataSize > 0)
            {
                var value = Assert.IsAssignableFrom<List<SubscribedMember>>(actionResult.Value);
                Assert.NotNull(value);
                Assert.Equal(expectedDataSize, value.Count);
            }
            else
            {
                Assert.IsType<NotFoundResult>(actionResult.Result);
            }
        }

        public static object[][] SuccessData => new object[][]
        {
            new object[] { DateTime.Now.AddDays(-30),DateTime.Now.AddDays(1),false,2 },
            new object[] { DateTime.Now.AddDays(-30), DateTime.Now.AddDays(1), true,3 },
            new object[] { DateTime.Now.AddDays(-30), DateTime.Now.AddDays(6), true,4 },
            new object[] { DateTime.Now.AddDays(3), DateTime.Now.AddDays(6), false,0 },
        };
    }
}
