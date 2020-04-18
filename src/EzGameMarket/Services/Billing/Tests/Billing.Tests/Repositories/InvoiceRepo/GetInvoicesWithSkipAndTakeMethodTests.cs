using Billing.API.Data;
using Billing.API.Services.Repositories.Implementations;
using Billing.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Billing.Tests.API.Repositories.InvoiceRepo
{
    public class GetInvoicesWithSkipAndTakeMethodTests
    {
        [Theory]
        [InlineData(0,30,3)]
        [InlineData(0,2,2)]
        [InlineData(1,1,1)]
        [InlineData(1,30,2)]
        [InlineData(60,30,0)]
        public async void GetInvoices_ShouldReturnSuccess(int skip, int take, int expectedCount)
        {
            //Arrange 
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{skip}-{take}-{expectedCount}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);
            var userInvoicesRepo = new UserInvoiceRepository(dbContext);

            //Act
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo);
            var actual = await repo.GetInvoices(skip,take);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedCount, actual.Count);
        }
    }
}
