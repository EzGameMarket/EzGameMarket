﻿using Billing.API.Data;
using Billing.API.Services.Repositories.Implementations;
using Billing.Tests.FakeImplementations;
using Xunit;

namespace Billing.API.Tests.Repositories.InvoiceRepo
{
    public class GetByIDMethodTests
    {
        [Theory]
        [InlineData(1, false)]
        [InlineData(5, true)]
        [InlineData(-1, true)]
        public async void GetInvoice_ShouldReturnSuccess(int id, bool isNull)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{id}-{isNull}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var userInvoicesRepo = new UserInvoiceRepository(dbContext);

            //Act
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, default);
            var actual = await repo.GetInvoceByID(id);

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