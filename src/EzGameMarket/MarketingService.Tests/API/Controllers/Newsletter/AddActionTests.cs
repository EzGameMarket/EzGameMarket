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
    public class AddActionTests
    {
        private DbContextOptions<MarketingDbContext> dbOptions;

        public AddActionTests()
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
            Created = DateTime.Now,
            ID = default,
            Message = "Sziasztok hali hello",
            Sended = DateTime.Now.AddDays(1),
            Title = "Hello World"
        };

        [Fact]
        public async void Add_ShouldReturnSuccessAndOneNewsletter()
        {
            //Act
            var dbContext = new MarketingDbContext(dbOptions);
            var repo = new NewsletterRepository(dbContext);

            var model = CreateModel();
            var id = await dbContext.Newsletters.CountAsync() + 1;

            //Arange
            var controller = new NewsletterController(repo);
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
            var repo = new NewsletterRepository(dbContext);

            var model = CreateModel();
            model.Title = string.Empty;

            //Arange
            var controller = new NewsletterController(repo);
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
            var repo = new NewsletterRepository(dbContext);

            var model = CreateModel();
            model.Title = default;

            //Arange
            var controller = new NewsletterController(repo);
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
            var repo = new NewsletterRepository(dbContext);

            var id = 1;
            var model = CreateModel();
            model.ID = id;

            //Arange
            var controller = new NewsletterController(repo);
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
            var repo = new NewsletterRepository(dbContext);

            var id = 1;
            var model = CreateModel();
            model.Title = "Ezt nem fogod elhinni!";

            //Arange
            var controller = new NewsletterController(repo);
            var actionResult = await controller.Add(model);
            var item = await repo.Get(id);

            //Assert
            Assert.NotNull(item);
            Assert.NotNull(actionResult);
            Assert.IsType<ConflictResult>(actionResult);
        }
    }
}
