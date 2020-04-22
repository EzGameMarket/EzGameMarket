using Billing.API.Data;
using Billing.API.Services.Repositories.Implementations;
using Billing.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Billing.API.Tests.Repositories.UserInvoiceRepo
{
    public class GetByUserIDMethodTests
    {
        [Theory]
        [InlineData("kriszw", false)]
        [InlineData("asdas", true)]
        [InlineData("", true)]
        public async void GetInvoice_ShouldReturnSuccess(string userID, bool isNull)
        {
            //Arrange 
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{userID}-{isNull}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            //Act
            var repo = new UserInvoiceRepository(dbContext);
            var actual = await repo.GetUserInvoice(userID);

            //Assert
            if (isNull)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
            }
        }
    }
}
