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
    public class GetInvoiceForUserWithOrderIDMethodTests
    {
        [Theory]
        [InlineData("kriszw", 1, false)]
        [InlineData("kriszw", 5, true)]
        [InlineData("kriszw", 100, true)]
        public async void GetInvoice_ShouldBeOkay(string userID, int orderID, bool expectedIsNull)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{userID}-{orderID}-{expectedIsNull}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);
            var userRepo = new UserInvoiceRepository(dbContext);
            var repo = new InvoiceRepository(dbContext, userRepo, default);


            //Act
            var service = new UserInvoiceService(repo, dbContext);
            var actual = await service.GetInvoicesForUserWithOrderID(userID, orderID);

            //Assert
            Assert.Equal(expectedIsNull, actual == default);
        }
    }
}
