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

namespace MarketingService.Tests.API.Controllers.Newsletter
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

                if (dbContext.Newsletters.Any() == false)
                {
                    dbContext.AddRange(FakeData.GetNewsletters());
                    dbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                dbContext.ChangeTracker.AcceptAllChanges();
            }
        }

        private NewsletterMessage CreateModel() => new NewsletterMessage()
        {
            Created = DateTime.Now.AddDays(-10),
            Sended = DateTime.Now,
            ID = 1,
            Message = "Még nincs itt a Half Life 3",
            Title = "Még mindig..."
        };

        [Fact]
        public async void Modify_ShouldReturnSuccessAndOneNewsletterWithID1()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new NewsletterRepository(dbContext);

            var id = 1;
            var model = CreateModel();
            model.ID = id;

            //Arange
            var controller = new NewsletterController(repo);
            var actionResult = await controller.Modify(id, model);
            var item = await repo.Get(id);

            //Assert
            Assert.NotNull(item);
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
            Assert.Equal(item.Title, model.Title);
        }
        [Fact]
        public async void Modify_ShouldReturnBadRequestForIDMinus1()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new NewsletterRepository(dbContext);

            var id = -1;
            var model = CreateModel();
            model.ID = id;

            //Arange
            var controller = new NewsletterController(repo);
            var actionResult = await controller.Modify(id, model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Modify_ShouldReturnBadRequestForInvalidTitle()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new NewsletterRepository(dbContext);

            var id = -1;
            var model = CreateModel();
            model.Title = default;

            //Arange
            var controller = new NewsletterController(repo);
            controller.ModelState.AddModelError("EMail", "Az email nem lehet null");
            var actionResult = await controller.Modify(id, model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }
        [Fact]
        public async void Modify_ShouldReturnNotFoundForID6()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new NewsletterRepository(dbContext);

            var id = 6;
            var model = CreateModel();
            model.ID = id;

            //Arange
            var controller = new NewsletterController(repo);
            var actionResult = await controller.Modify(id, model);
            var item = await repo.Get(id);

            //Assert
            Assert.Null(item);
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
