using Billing.API.Controllers;
using Billing.API.Data;
using Billing.API.Models;
using Billing.API.Services.Repositories.Implementations;
using Billing.API.Services.Services.Implementations;
using Billing.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Services.API.Communication.Models;
using Shared.Utiliies.CloudStorage.Shared.Models.BaseResult;
using Shared.Utilities.CloudStorage.Shared.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Billing.API.Tests.Controllers.UserInvoice
{
    public class DownloadInvoiceFromUserIDAndOrderIDActionTests
    {
        [Theory]
        [InlineData("kriszw", 1)]
        public async void Download_ShouldReturnSuccess(string userID, int orderID)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName+$"{userID}-{orderID}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var userInvoicesRepo = new UserInvoiceRepository(dbContext);
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, default);
            var userInvoicesService = new UserInvoiceService(repo, dbContext);

            var file = await userInvoicesService.DownloadInvoice(orderID);

            var mockedStorageService = new Mock<IStorageService>();
            mockedStorageService.Setup(s => s.Download(file.FileUri)).ReturnsAsync(new CloudStorageDownloadResult(true, new MemoryStream()));

            //Act
            var controller = new UserInvoiceController(repo, userInvoicesService, mockedStorageService.Object);
            var actionResult = await controller.DownloadInvoice(userID, orderID);

            //Assert
            Assert.NotNull(actionResult);
            var actual = Assert.IsAssignableFrom<FileStreamResult>(actionResult);
            Assert.NotNull(actual);
            Assert.Equal(0,actual.FileStream.Length);
        }

        [Theory]
        [InlineData("", 1,  typeof(BadRequestObjectResult))]
        [InlineData("asd", 0, typeof(BadRequestObjectResult))]
        [InlineData("asdsadasdadsa", 100, typeof(NotFoundResult))]
        public async void Download_ShouldFail(string userID, int orderID, Type expectedActionResultObjectType)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName+$"{userID}-{orderID}-{expectedActionResultObjectType.FullName}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var userInvoicesRepo = new UserInvoiceRepository(dbContext);
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, default);
            var userInvoicesService = new UserInvoiceService(repo, dbContext);

            //Act
            var controller = new UserInvoiceController(repo, userInvoicesService, default);
            var actionResult = await controller.DownloadInvoice(userID, orderID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType(expectedActionResultObjectType,actionResult);
        }
    }
}
