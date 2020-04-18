using Billing.API.Data;
using Billing.API.Services.Repositories.Implementations;
using Billing.Tests.FakeImplementations;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Billing.Tests.API.Repositories.UserInvoiceRepo
{
    public class AnyWithUserIDMethodTests
    {
        [Theory]
        [InlineData("kriszw", true)]
        [InlineData("asdas", false)]
        [InlineData("", false)]
        public async void AnyWithUserID_ShouldReturnSuccess(string userID, bool expected)
        {
            //Arrange 
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{userID}-{expected}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            //Act
            var repo = new UserInvoiceRepository(dbContext);
            var actual = await repo.AnyUserInvoiceWithUserID(userID);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
