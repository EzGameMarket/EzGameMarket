using Billing.API.Controllers;
using Billing.API.Data;
using Billing.API.Models;
using Billing.API.Services.Repositories.Implementations;
using Billing.API.Services.Services.Implementations;
using Billing.API.ViewModels;
using Billing.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Pagination;
using Shared.Services.API.Communication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Xunit;

namespace Billing.API.Tests.Controllers.UserInvoice
{
    public class GetInvoiceFromUserIDAndOrderIDActionTests
    {
        [Theory]
        [InlineData("kriszw", 1)]
        public async void GetInvoice_ShouldReturnSuccess(string userID, int orderID)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{userID}-{orderID}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var userInvoicesRepo = new UserInvoiceRepository(dbContext);
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, default);
            var userInvoicesService = new UserInvoiceService(repo, dbContext);

            //Act
            var controller = new UserInvoiceController(repo, userInvoicesService, default);
            var actionResult = await controller.GetInvoice(userID, orderID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<APIResponse<Invoice>>>(actionResult);
            var response = Assert.IsAssignableFrom<APIResponse<Invoice>>(actionResult.Value);
            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.NotNull(response.Data);
        }

        [Theory]
        [InlineData("", 1, "A UserID nem lehet üres", typeof(BadRequestObjectResult))]
        [InlineData("asd", 0, "Az OrderID nem lehet kisebb mint 0", typeof(BadRequestObjectResult))]
        [InlineData("asdsadasdadsa", 100, "A asdsadasdadsa felhasználónak nem létezik számlája a 100 rendelés azonosítóval", typeof(NotFoundObjectResult))]
        public async void GetInvoice_ShouldFail(string userID, int orderID, string expectedMSG, Type expectedActionResultObjectType)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{userID}-{orderID}-{expectedMSG}-{expectedActionResultObjectType.FullName}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var userInvoicesRepo = new UserInvoiceRepository(dbContext);
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, default);
            var userInvoicesService = new UserInvoiceService(repo, dbContext);

            //Act
            var controller = new UserInvoiceController(repo, userInvoicesService, default);
            var actionResult = await controller.GetInvoice(userID, orderID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<APIResponse<Invoice>>>(actionResult);
            Assert.IsType(expectedActionResultObjectType, actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<Invoice>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
