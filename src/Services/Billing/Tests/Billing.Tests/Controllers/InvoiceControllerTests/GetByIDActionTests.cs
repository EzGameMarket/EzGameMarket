using Billing.API.Controllers;
using Billing.API.Data;
using Billing.API.Models;
using Billing.API.Services.Repositories.Implementations;
using Billing.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Shared.Services.API.Communication.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Billing.API.Tests.Controllers.InvoiceControllerTests
{
    public class GetByIDActionTests
    {
        [Fact]
        public async void GetInvoice_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var id = 1;

            var userInvoicesRepo = new UserInvoiceRepository(dbContext);
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, default);

            //Act
            var controller = new InvoiceController(repo);
            var actionResult = await controller.GetByID(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<APIResponse<Invoice>>>(actionResult);
            var response = Assert.IsAssignableFrom<APIResponse<Invoice>>(actionResult.Value);
            Assert.True(response.Success);
            var actual = Assert.IsAssignableFrom<Invoice>(response.Data);
            Assert.Equal(id, actual.ID);
        }

        [Fact]
        public async void GetInvoice_ShouldReturnBadRequestForIDMinus1()
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
            var actionResult = await controller.GetByID(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<APIResponse<Invoice>>>(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void GetInvoice_ShouldReturnSuccessNotFoundForID100()
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
            var actionResult = await controller.GetByID(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<APIResponse<Invoice>>>(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
