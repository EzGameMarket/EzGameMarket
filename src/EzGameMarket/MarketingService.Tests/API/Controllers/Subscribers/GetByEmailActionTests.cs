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
    public class GetByEmailActionTests
    {
        private DbContextOptions<MarketingDbContext> dbOptions;

        public GetByEmailActionTests()
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
        public async void GetByEmail_ShouldReturnSuccessAnd1Member()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);

            var email = "werdnikkrisz@gmail.com";

            //Arange
            var controller = new SubscribersController(repo);
            var actionResult = await controller.GetByEmail(email);
            var item = await repo.GetByEmail(email);

            //Assert
            Assert.NotNull(item);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<SubscribedMember>>(actionResult);
            var value = Assert.IsAssignableFrom<SubscribedMember>(actionResult.Value);

            Assert.NotNull(value);
            Assert.Equal(item.EMail, value.EMail);
        }
        [Fact]
        public async void GetByEmail_ShouldReturnBadRequestForEmailIsEmptyString()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);

            var email = string.Empty;

            //Arange
            var controller = new SubscribersController(repo);
            var actionResult = await controller.GetByEmail(email);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }
        [Fact]
        public async void GetByEmail_ShouldReturnNotFoundForASD()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);

            var email = "asd@gmail.com";

            //Arange
            var controller = new SubscribersController(repo);
            var actionResult = await controller.GetByEmail(email);
            var item = await repo.GetByEmail(email);

            //Assert
            Assert.Null(item);
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
