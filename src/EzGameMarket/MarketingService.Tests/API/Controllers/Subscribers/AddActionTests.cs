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
                    dbContext.AddRange(FakeData.GetMembers());
                    dbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                dbContext.ChangeTracker.AcceptAllChanges();
            }
        }

        private SubscribedMember CreateModel() => new SubscribedMember()
        {
            Active = true,
            EMail = "werdnikkrisz.test2@gmail.com",
            SubscribedDate = DateTime.Now,
            UnSubscribedDate = default
        };

        [Fact]
        public async void Add_ShouldReturnSuccessAndOneSubscriber()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);

            var model = CreateModel();
            var id = await dbContext.Members.CountAsync() + 1;

            //Arange
            var controller = new SubscribersController(repo);
            var actionResult = await controller.Add(model);
            var item = await repo.Get(id);

            //Assert
            Assert.NotNull(item);
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
            Assert.Equal(item.EMail, model.EMail);
        }
        [Fact]
        public async void Add_ShouldReturnBadRequestForEmailIsEmptyString()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);

            var model = CreateModel();
            model.EMail = string.Empty;

            //Arange
            var controller = new SubscribersController(repo);
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Add_ShouldReturnBadRequestForInvalidEmailWithoutModelValidation()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);

            var model = CreateModel();
            model.EMail = "hello bello";

            //Arange
            var controller = new SubscribersController(repo);
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Add_ShouldReturnBadRequestForInvalidEmail()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);

            var model = CreateModel();
            model.EMail = default;

            //Arange
            var controller = new SubscribersController(repo);
            controller.ModelState.AddModelError("EMail", "Az email nem lehet null");
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
            var repo = new SubscriberRepository(dbContext);

            var id = 1;
            var model = CreateModel();
            model.ID = id;

            //Arange
            var controller = new SubscribersController(repo);
            var actionResult = await controller.Add(model);
            var item = await repo.Get(id);

            //Assert
            Assert.NotNull(item);
            Assert.NotNull(actionResult);
            Assert.IsType<ConflictResult>(actionResult);
        }

        [Fact]
        public async void Add_ShouldReturnConflictForwerdnikkriszatgmaildotcom()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);

            var id = 1;
            var model = CreateModel();
            model.EMail = "werdnikkrisz@gmail.com";

            //Arange
            var controller = new SubscribersController(repo);
            var actionResult = await controller.Add(model);
            var item = await repo.Get(id);

            //Assert
            Assert.NotNull(item);
            Assert.NotNull(actionResult);
            Assert.IsType<ConflictResult>(actionResult);
        }
    }
}
