using Billing.API.Controllers;
using Billing.API.Data;
using Billing.API.Models;
using Billing.API.Services.Repositories.Implementations;
using Billing.API.Services.Services.Abstractions;
using Billing.API.ViewModels;
using Billing.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Moq;
using Shared.Services.API.Communication.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Xunit;

namespace Billing.API.Tests.Controllers.InvoiceControllerTests
{
    public class CreateActionTests
    {
        private InvoiceCreationViewModel CreateModel() => new InvoiceCreationViewModel()
        {
            UserID = "test",
            IsCanceledInvoice = false,
            Invoice = new Invoice()
            {
                ID = default,
                OrderID = 10
            }
        };

        [Fact]
        public async void Create_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var mockedInvoiceService = new Mock<IInvoiceService>();
            mockedInvoiceService.Setup(i => i.Create(default));

            var model = CreateModel();

            var userInvoicesRepo = new UserInvoiceRepository(dbContext);
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, mockedInvoiceService.Object);

            //Act
            var controller = new InvoiceController(repo);
            var actionResult = await controller.Create(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<EmptyAPIResponse>>(actionResult);
            var actual = Assert.IsAssignableFrom<EmptyAPIResponse>(actionResult.Value);
            Assert.True(actual.Success);
        }

        [Theory]
        [MemberData(nameof(CreateAddInvalidTestData))]
        public async void Create_ShouldFail(InvoiceCreationViewModel model, string expectedMSG, Type expectedActionResultObjectType)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var userInvoicesRepo = new UserInvoiceRepository(dbContext);
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, default);

            //Act
            var controller = new InvoiceController(repo);
            var actionResult = await controller.Create(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<EmptyAPIResponse>>(actionResult);
            Assert.IsType(expectedActionResultObjectType, actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<EmptyAPIResponse>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }

        public static object[][] CreateAddInvalidTestData() => new object[][]
        {
            new object[] { new InvoiceCreationViewModel() { UserID = "", Invoice = new Invoice()  { ID = default, OrderID = 10 } }, "A Felhasználó azonosító nem lehet üres", typeof(BadRequestObjectResult) },
            new object[] { new InvoiceCreationViewModel() { UserID = "test", Invoice = new Invoice()  { ID = default, OrderID = -1 } }, "A rendelés azonosító nem lehet kisebb mint 0", typeof(BadRequestObjectResult) },
            new object[] { new InvoiceCreationViewModel() { UserID = "test", Invoice = new Invoice()  { ID = default, OrderID = 1 } }, "A 1 rendelés azonosítóhoz már létezik egy számla", typeof(ConflictObjectResult) },
            new object[] { new InvoiceCreationViewModel() { UserID = "kriszw", Invoice = new Invoice()  { ID = 1, OrderID = 10 } }, "A 1 azonosítóvál már létezik egy számla", typeof(ConflictObjectResult) },
        };
    }
}
