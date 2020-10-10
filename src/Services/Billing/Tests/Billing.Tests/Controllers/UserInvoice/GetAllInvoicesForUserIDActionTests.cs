using Billing.API.Controllers;
using Billing.API.Data;
using Billing.API.Models;
using Billing.API.Services.Repositories.Implementations;
using Billing.API.Services.Services.Implementations;
using Billing.API.ViewModels;
using Billing.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using Shared.Services.API.Communication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Billing.API.Tests.Controllers.UserInvoice
{
    public class GetAllInvoicesForUserIDActionTests
    {
        [Theory]
        [InlineData("kriszw",2)]
        [InlineData("asdasdsa",0)]
        public async void GetAllInvoices_ShouldReturnSuccess(string userID, int expectedCount)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var userInvoicesRepo = new UserInvoiceRepository(dbContext);
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, default);
            var userInvoicesService = new UserInvoiceService(repo, dbContext);

            //Act
            var controller = new UserInvoiceController(repo, userInvoicesService, default);
            var actionResult = await controller.GetAllInvoice(userID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<APIResponse<IEnumerable<PaginatedInvoiceViewModel>>>>(actionResult);
            var response = Assert.IsAssignableFrom<APIResponse<IEnumerable<PaginatedInvoiceViewModel>>>(actionResult.Value);
            Assert.True(response.Success);
            

            if (expectedCount > 0)
            {
                var actual = Assert.IsAssignableFrom<IEnumerable<PaginatedInvoiceViewModel>>(response.Data);
                Assert.Equal(expectedCount, actual.Count());
            }
            else
            {
                Assert.Null(response.Data);
            }
        }

        [Theory]
        [InlineData("","A UserID nem lehet üres",typeof(BadRequestObjectResult))]
        public async void GetAllInvoices_ShouldFail(string userID, string expectedMSG, Type expectedActionResultObjectType)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var userInvoicesRepo = new UserInvoiceRepository(dbContext);
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, default);
            var userInvoicesService = new UserInvoiceService(repo, dbContext);

            //Act
            var controller = new UserInvoiceController(repo, userInvoicesService, default);
            var actionResult = await controller.GetAllInvoice(userID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<APIResponse<IEnumerable<PaginatedInvoiceViewModel>>>>(actionResult);
            Assert.IsType(expectedActionResultObjectType, actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<IEnumerable<PaginatedInvoiceViewModel>>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
