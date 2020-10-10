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
    public class GetPaginatedInvoiceModelsWithSkipAndTakeMethodTests
    {
        [Theory]
        [InlineData("kriszw", 0, 30, 2)]
        [InlineData("kriszw", 0, 1, 1)]
        [InlineData("kriszw", 1, 1, 1)]
        [InlineData("asdsda", 0, 30, 0)]
        public async void GetInvoicesWithSkipAndTake_ShouldBeOkay(string userID, int skip, int take, int expectedCount)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{userID}-{skip}-{take}-{expectedCount}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);
            var userRepo = new UserInvoiceRepository(dbContext);
            var repo = new InvoiceRepository(dbContext, userRepo, default);


            //Act
            var service = new UserInvoiceService(repo, dbContext);
            var actual = await service.GetPaginatedInvoicesForUser(userID, skip, take);

            //Assert
            if (expectedCount > 0)
            {
                Assert.Equal(expectedCount, actual.Data.Count());
            }
            else
            {
                Assert.Null(actual.Data);
            }
        }
    }
}
