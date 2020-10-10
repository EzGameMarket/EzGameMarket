using Billing.API.Data;
using Billing.API.Services.Repositories.Implementations;
using Billing.Tests.FakeImplementations;
using Xunit;

namespace Billing.API.Tests.Repositories.InvoiceRepo
{
    public class GetInvoicesByUserIDWithSkipAndTakeMethodTests
    {
        [Theory]
        [InlineData(0, 30, "kriszw", 2)]
        [InlineData(0, 30, "test", 1)]
        [InlineData(0, 2, "kriszw", 2)]
        [InlineData(1, 1, "kriszw", 1)]
        [InlineData(1, 30, "kriszw", 1)]
        [InlineData(60, 30, "kriszw", 0)]
        [InlineData(0, 30, "asdasd", 0)]
        public async void GetInvoices_ShouldReturnSuccess(int skip, int take, string userID, int expectedCount)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{skip}-{take}-{expectedCount}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);
            var userInvoicesRepo = new UserInvoiceRepository(dbContext);

            //Act
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, default);
            var actual = await repo.GetInvoicesByUserIDWithSkipAndTake(userID, skip, take);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedCount, actual.Count);
        }
    }
}