using Billing.API.Data;
using Billing.API.Exceptions.Invoices;
using Billing.API.Services.Repositories.Implementations;
using Billing.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using Xunit;

namespace Billing.API.Tests.Repositories.InvoiceRepo
{
    public class UpdateFilePathMethodTests
    {
        [Theory]
        [InlineData("catalog/sms3/acvasDFSAasd.png", 1)]
        public async void Update_ShouldBeOkay(string fileURL, int invoiceID)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{fileURL}-{invoiceID}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var userInvoicesRepo = new UserInvoiceRepository(dbContext);

            //Act
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, default);
            await repo.UpdateFileURL(fileURL, invoiceID);
            var actual = await repo.GetInvoceByID(invoiceID);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(fileURL, actual.File.FileUri);
        }

        [Fact]
        public async void Update_ShouldThrowInvoiceNotFoundException()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var userInvoicesRepo = new UserInvoiceRepository(dbContext);

            //Act
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, default);
            var updateTask = repo.UpdateFileURL("test.png", 100);

            //Assert
            await Assert.ThrowsAsync<InvoiceNotFoundByIDException>(() => updateTask);
        }
    }
}
