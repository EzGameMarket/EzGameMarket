using Billing.API.Data;
using Billing.API.Exceptions.Orders;
using Billing.API.Services.Repositories.Implementations;
using Billing.API.Services.Services.Implementations;
using Billing.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Billing.API.Tests.Services.InvoiceValidation
{
    public class IsUsersInvoiceMethodTests
    {
        [Theory]
        [InlineData("kriszw", 1, true)]
        [InlineData("kriszw", 5, false)]
        public async void IsUsersOrder_ShouldBeOkay(string userID, int orderID, bool expectedResult)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{userID}-{orderID}-{expectedResult}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            //Act
            var service = new InvoiceValidationService(dbContext);
            var actual = await service.IsUsersOrder(userID, orderID);

            //Assert
            Assert.Equal(expectedResult, actual);
        }
    }
}
