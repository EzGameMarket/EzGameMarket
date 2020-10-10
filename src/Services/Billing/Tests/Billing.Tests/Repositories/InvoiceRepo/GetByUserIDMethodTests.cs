using Billing.API.Data;
using Billing.API.Services.Repositories.Implementations;
using Billing.Tests.FakeImplementations;
using Xunit;

namespace Billing.API.Tests.Repositories.InvoiceRepo
{
    public class GetByUserIDMethodTests
    {
        [Theory]
        [InlineData("kriszw", 2)]
        [InlineData("tst", 0)]
        public async void GetInvoice_ShouldReturnSuccess(string userName, int expectedCount)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{userName}-{expectedCount}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);
            var userInvoicesRepo = new UserInvoiceRepository(dbContext);

            //Act
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, default);
            var actual = await repo.GetInvoicesByUserID(userName);

            //Assert
            Assert.Equal(expectedCount, actual.Count);
        }
    }
}