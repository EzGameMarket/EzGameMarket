using Billing.API.Controllers;
using Billing.API.Data;
using Billing.API.Services.Repositories.Implementations;
using Billing.API.Services.Services.Implementations;
using Billing.API.ViewModels;
using Billing.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Pagination;
using Shared.Services.API.Communication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Billing.API.Tests.Controllers.UserInvoice
{
    public class GetPaginatedInvoicesForUserActionTests
    {
        [Theory]
        [InlineData("kriszw",0,30, 2,2)]
        public async void GetInvoiceWithSkipAndTake_ShouldReturnSuccess(string userID, int pageIndex, int pageSize, int expectedCount, int expectedTotalCount)
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
            var actionResult = await controller.GetInvoices(userID, pageIndex, pageSize);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<APIResponse<PaginationViewModel<PaginatedInvoiceViewModel>>>>(actionResult);
            var response = Assert.IsAssignableFrom<APIResponse<PaginationViewModel<PaginatedInvoiceViewModel>>>(actionResult.Value);
            Assert.True(response.Success);
            var actual = Assert.IsAssignableFrom<PaginationViewModel<PaginatedInvoiceViewModel>>(response.Data);
            Assert.Equal(expectedCount, actual.Data.Count());
            Assert.Equal(expectedTotalCount, actual.TotalItemsCount);
        }

        [Theory]
        [InlineData("",0,30, "A UserID nem lehet üres", typeof(BadRequestObjectResult))]
        [InlineData("asdsadasdadsa",0,30, "A asdsadasdadsa felhasználó nem létezik", typeof(NotFoundObjectResult))]
        [InlineData("kriszw",2,1, "A kriszw userhez nincs több mint 2 darab számla", typeof(NotFoundObjectResult))]
        public async void GetInvoiceWithSkipAndTake_ShouldFail(string userID,int pageIndex, int pageSize, string expectedMSG, Type expectedActionResultObjectType)
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
            var actionResult = await controller.GetInvoices(userID, pageIndex, pageSize);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<APIResponse<PaginationViewModel<PaginatedInvoiceViewModel>>>>(actionResult);
            Assert.IsType(expectedActionResultObjectType, actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<APIResponse<PaginationViewModel<PaginatedInvoiceViewModel>>>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }
    }
}
