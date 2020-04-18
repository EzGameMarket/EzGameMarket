using Billing.API.Data;
using Billing.API.Services.Repositories.Implementations;
using Billing.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Billing.Tests.API.Repositories.InvoiceRepo
{
    public class StornoMethodTests
    {
        [Fact]
        public async void Storno_ShouldReturnSuccess()
        {
            //Arrange 
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);
            var userInvoicesRepo = new UserInvoiceRepository(dbContext);

            var id = 2;

            //Act
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo);
            await repo.Storno(id);
            var actual = await repo.GetInvoceByID(id);

            //Assert
            Assert.NotNull(actual);
            Assert.True(actual.Canceled);
        }
    }
}
