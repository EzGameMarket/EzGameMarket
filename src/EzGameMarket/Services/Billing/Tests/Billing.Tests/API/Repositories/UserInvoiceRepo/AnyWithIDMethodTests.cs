using Billing.API.Data;
using Billing.API.Services.Repositories.Implementations;
using Billing.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Billing.Tests.API.Repositories.UserInvoiceRepo
{
    public class AnyWithIDMethodTests
    {
        [Theory]
        [InlineData(1, true)]
        [InlineData(6, false)]
        [InlineData(-1, false)]
        public async void AnyWithUserID_ShouldReturnSuccess(int id, bool expected)
        {
            //Arrange 
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{id}-{expected}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            //Act
            var repo = new UserInvoiceRepository(dbContext);
            var actual = await repo.AnyUserInvoiceWithID(id);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
