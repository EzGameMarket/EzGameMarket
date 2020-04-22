using Billing.API.Data;
using Billing.API.Services.Repositories.Implementations;
using Billing.API.Services.Services.Abstractions;
using Billing.Tests.FakeImplementations;
using Moq;
using Xunit;

namespace Billing.API.Tests.Repositories.InvoiceRepo
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
            var mockedInvoiceService = new Mock<IInvoiceService>();
            mockedInvoiceService.Setup(i => i.Storno(default));

            var id = 2;

            //Act
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, mockedInvoiceService.Object);
            await repo.Storno(id);
            var actual = await repo.GetInvoceByID(id);

            //Assert
            Assert.NotNull(actual);
            Assert.True(actual.Canceled);
        }
    }
}