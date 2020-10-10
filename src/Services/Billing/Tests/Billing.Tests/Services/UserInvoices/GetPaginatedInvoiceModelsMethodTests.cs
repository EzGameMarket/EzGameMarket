using Billing.API.Data;
using Billing.API.Services.Repositories.Implementations;
using Billing.API.Services.Services.Implementations;
using Billing.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Billing.API.Tests.Services.UserInvoices
{
    public class GetPaginatedInvoiceModelsMethodTests
    {
        [Theory]
        [InlineData("kriszw", 2)]
        [InlineData("asdads", 0)]
        public async void GetAll_ShouldBeOkay(string userID, int expectedCount)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);
            var userRepo = new UserInvoiceRepository(dbContext);
            var repo = new InvoiceRepository(dbContext, userRepo, default);


            //Act
            var service = new UserInvoiceService(repo, dbContext);
            var actual = await service.GetAllPaginatedInvoice(userID);

            //Assert
            if (expectedCount > 0)
            {
                Assert.Equal(expectedCount, actual.Count());
            }
            else
            {
                Assert.Null(actual);
            }
        }
    }
}
