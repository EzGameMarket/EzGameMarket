using Billing.API.Data;
using Billing.API.Exceptions.Invoices;
using Billing.API.Services.Repositories.Implementations;
using Billing.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Billing.API.Tests.Repositories.InvoiceRepo
{
    public class UploadBillingSystemIDMethodTests
    {
        [Theory]
        [InlineData("2020-0001", 1)]
        public async void Update_ShouldBeOkay(string billingSystemID, int invoiceID)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{billingSystemID}-{invoiceID}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var userInvoicesRepo = new UserInvoiceRepository(dbContext);

            //Act
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, default);
            await repo.UploadBillingSystemID(billingSystemID, invoiceID);
            var actual = await repo.GetInvoceByID(invoiceID);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(billingSystemID, actual.BillingSystemInvoiceID);
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
            var updateTask = repo.UploadBillingSystemID("test", 100);
            
            //Assert
            await Assert.ThrowsAsync<InvoiceNotFoundByIDException>(() => updateTask);
        }
    }
}
