using Billing.API.Data;
using Billing.API.Exceptions.UserInvoice;
using Billing.API.Models;
using Billing.API.Services.Repositories.Implementations;
using Billing.API.ViewModels;
using Billing.Tests.FakeImplementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Billing.API.Tests.Repositories.UserInvoiceRepo
{
    public class AddMethodTests
    {
        [Fact]
        public async void Add_ShouldReturnSuccess()
        {
            //Arrange 
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var model = new UserInvoice()
            {
                UserID = "testsnewADD"
            };
            var expectedID = (await dbContext.UserInvoices.MaxAsync(i => i.ID)) + 1;

            //Act
            var repo = new UserInvoiceRepository(dbContext);
            await repo.Add(model);
            var actual = await repo.GetUserInvoice(model.UserID);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expectedID, actual.ID);
            Assert.Equal(model.UserID, actual.UserID);
        }

        [Theory]
        [MemberData(nameof(CreateExceptionTestData))]
        public async void Add_ShouldThrowException(UserInvoice model, Type expectionType)
        {
            //Arrange 
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            //Act
            var repo = new UserInvoiceRepository(dbContext);
            var uploadTask = repo.Add(model);

            //Assert
            await Assert.ThrowsAsync(expectionType, () => uploadTask);
        }

        public static object[][] CreateExceptionTestData() => new object[][]
        {
            new object[]
            {
                new UserInvoice() 
                {
                    ID = 1,
                    UserID = "testnewID"
                },
                typeof(UserInvoiceAlreadyExistsWithIDException)
            },
            new object[]
            {
                new UserInvoice()
                {
                    UserID = "kriszw"
                },
                typeof(UserInvoiceAlreadyExistsWithUserIDException)
            }
        };
    }
}
