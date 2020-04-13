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
    public class ModifyActionTests
    {
        private DbContextOptions<MarketingDbContext> dbOptions;

        public ModifyActionTests()
        {
            dbOptions = new DbContextOptionsBuilder<MarketingDbContext>()
                .UseInMemoryDatabase(databaseName: $"db-marketing-test-{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName}")
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
            EMail = "werdnikkrisz.test@gmail.com",
            SubscribedDate = DateTime.Now,
            UnSubscribedDate = default
        };

        [Fact]
        public async void Modify_ShouldReturnSuccessAndOneSubscriberWithID1()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);

            var id = 1;
            var model = CreateModel();
            model.ID = id;

            //Arange
            var controller = new SubscribersController(repo);
            var actionResult = await controller.Modify(id, model);
            var item = await repo.Get(id);

            //Assert
            Assert.NotNull(item);
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
            Assert.Equal(item.EMail, model.EMail);
        }
        [Fact]
        public async void Modify_ShouldReturnBadRequestForIDMinus1()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);

            var id = -1;
            var model = CreateModel();
            model.ID = id;

            //Arange
            var controller = new SubscribersController(repo);
            var actionResult = await controller.Modify(id,model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Modify_ShouldReturnBadRequestForInvalidEmail()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);

            var id = -1;
            var model = CreateModel();
            model.EMail = default;

            //Arange
            var controller = new SubscribersController(repo);
            controller.ModelState.AddModelError("EMail","Az email nem lehet null");
            var actionResult = await controller.Modify(id, model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }
        [Fact]
        public async void Modify_ShouldReturnNotFoundForID100()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new SubscriberRepository(dbContext);

            var id = 100;
            var model = CreateModel();
            model.ID = id;

            //Arange
            var controller = new SubscribersController(repo);
            var actionResult = await controller.Modify(id, model);
            var item = await repo.Get(id);

            //Assert
            Assert.Null(item);
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
