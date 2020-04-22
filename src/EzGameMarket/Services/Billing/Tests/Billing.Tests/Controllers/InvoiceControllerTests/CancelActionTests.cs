using Billing.API.Controllers;
using Billing.API.Data;
using Billing.API.Models;
using Billing.API.Services.Repositories.Implementations;
using Billing.API.Services.Services.Abstractions;
using Billing.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Services.API.Communication.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Billing.API.Tests.Controllers.InvoiceControllerTests
{
    public class CancelActionTests
    {
        [Fact]
        public async void Cancel_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var mockedInvoiceService = new Mock<IInvoiceService>();
            mockedInvoiceService.Setup(i => i.Storno(default));

            var id = 1;

            var userInvoicesRepo = new UserInvoiceRepository(dbContext);
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, mockedInvoiceService.Object);

            //Act
            var controller = new InvoiceController(repo);
            var actionResult = await controller.Cancel(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<EmptyAPIResponse>>(actionResult);
            var actual = Assert.IsAssignableFrom<EmptyAPIResponse>(actionResult.Value);
            Assert.True(actual.Success);
        }

        [Fact]
        public async void Cancel_ShouldReturnBadRequestForIDMinus1()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var id = -1;

            var userInvoicesRepo = new UserInvoiceRepository(dbContext);
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, default);

            //Act
            var controller = new InvoiceController(repo);
            var actionResult = await controller.Cancel(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void Cancel_ShouldReturnSuccessNotFoundForID100()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var id = 100;

            var userInvoicesRepo = new UserInvoiceRepository(dbContext);
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, default);

            //Act
            var controller = new InvoiceController(repo);
            var actionResult = await controller.Cancel(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
