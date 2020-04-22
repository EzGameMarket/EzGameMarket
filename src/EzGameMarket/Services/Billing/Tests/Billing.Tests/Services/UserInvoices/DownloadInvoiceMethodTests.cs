using Billing.API.Data;
using Billing.API.Services.Repositories.Implementations;
using Billing.API.Services.Services.Implementations;
using Billing.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Billing.API.Tests.Services.UserInvoices
{
    public class DownloadInvoiceMethodTests
    {
        [Theory]
        [InlineData(1, false)]
        public async void Download_ShouldBeOkay(int orderID, bool expectedIsNull)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{orderID}-{expectedIsNull}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);
            var userRepo = new UserInvoiceRepository(dbContext);
            var repo = new InvoiceRepository(dbContext, userRepo, default);


            //Act
            var service = new UserInvoiceService(repo, dbContext);
            var actual = await service.DownloadInvoice(orderID);

            //Assert
            if (expectedIsNull == false)
            {
                Assert.NotNull(actual);
                Assert.NotNull(actual.FileUri);
            }
            else
            {
                Assert.Null(actual);
            }
        }
    }
}
