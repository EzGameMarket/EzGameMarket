using Billing.API.Data;
using Billing.API.Services.Repositories.Implementations;
using Billing.Tests.FakeImplementations;
using Xunit;

namespace Billing.API.Tests.Repositories.InvoiceRepo
{
    public class AnyWithOrderIDMethodTests
    {
        [Theory]
        [InlineData(1, true)]
        [InlineData(100, false)]
        [InlineData(-1, false)]
        public async void AnyWithID_ShouldReturnSuccess(int orderID, bool expected)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{orderID}-{expected}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var userInvoicesRepo = new UserInvoiceRepository(dbContext);

            //Act
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, default);
            var actual = await repo.AnyWithOrderID(orderID);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}