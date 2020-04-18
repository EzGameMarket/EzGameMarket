using Billing.API.Data;
using Billing.API.Exceptions.UserInvoice;
using Billing.API.Models;
using Billing.API.Services.Repositories.Implementations;
using Billing.Tests.FakeImplementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Billing.Tests.API.Repositories.UserInvoiceRepo
{
    public class AddInvoiceMethodTests
    {
        [Fact]
        public async void AddNewInvoiceToUserID_ShouldReturnSuccess()
        {
            //Arrange 
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var userID = "kriszw";
            var invoice = new Invoice()
            {
                OrderID = 100,
                Address = "Görbehalom Csillés köz 3",
                City = "Sopron",
                FirstName = "Krisztián",
                Country = "Hungary",
                PostCode = "9408",
                LastName = "Werdnik",
                Total = 24112,
                DueDate = DateTime.Now,
                FullfiledDate = DateTime.Now,
                Items = new List<InvoiceItem>()
                        {
                            new InvoiceItem()
                            {
                                BruttoPrice = 1270,
                                NetPrice = 1000,
                                Description = "CSGO kulcs",
                                Name = "CSGO",
                                Quantity = 10,
                                ProductID = "csgo"
                            },
                            new InvoiceItem()
                            {
                                BruttoPrice = 12700,
                                NetPrice = 10000,
                                Description = "Battlefield V kulcs",
                                Name = "battlefield v",
                                Quantity = 4,
                                ProductID = "bfv"
                            }
                        },
                FileID = 5,
                File = new InvoiceFile()
                {
                    FileUri = "cdn.kwsoft.dev/invoices/c.pdf",
                }
            };
            
            //Act
            var repo = new UserInvoiceRepository(dbContext);

            var expectedSize = (await repo.GetUserInvoice(userID)).Invoices.Count + 1;

            await repo.AddInvoiceForUserID(userID, invoice);
            var actual = await repo.GetUserInvoice(userID);


            //Assert
            Assert.NotNull(actual);
            Assert.Equal(userID, actual.UserID);
            Assert.Equal(expectedSize, actual.Invoices.Count);
        }

        [Theory]
        [MemberData(nameof(CreateExceptionTestData))]
        public async void Add_ShouldThrowException(string userID, Invoice model, Type expectionType)
        {
            //Arrange 
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            //Act
            var repo = new UserInvoiceRepository(dbContext);
            var uploadTask = repo.AddInvoiceForUserID(userID,model);

            //Assert
            await Assert.ThrowsAsync(expectionType, () => uploadTask);
        }

        public static object[][] CreateExceptionTestData() => new object[][]
        {
            new object[]
            {
                "asdasdad",
                new Invoice()
                {
                    OrderID = 1
                },
                typeof(UserInvoiceNotFoundByUserIDException)
            },
            new object[]
            {
                "kriszw",
                new Invoice()
                {
                    OrderID = 1
                },
                typeof(UserInvoiceNewInvoiceWithOrderIDForUserIDAlreadyAddedException)
            }
        };
    }
}
