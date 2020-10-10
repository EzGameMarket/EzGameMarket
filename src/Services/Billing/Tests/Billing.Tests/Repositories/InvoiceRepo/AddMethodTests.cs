using Billing.API.Data;
using Billing.API.Exceptions.Invoices;
using Billing.API.Models;
using Billing.API.Services.Repositories.Implementations;
using Billing.API.Services.Services.Abstractions;
using Billing.API.ViewModels;
using Billing.Tests.FakeImplementations;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Billing.API.Tests.Repositories.InvoiceRepo
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

            var userInvoicesRepo = new UserInvoiceRepository(dbContext);

            var model = new InvoiceCreationViewModel()
            {
                UserID = "kriszw",
                Invoice = new Invoice()
                {
                    OrderID = 10,
                    Address = "Görbehalom Csillés köz 1",
                    City = "Sopron",
                    FirstName = "Krisztián",
                    Country = "Hungary",
                    PostCode = "9408",
                    LastName = "Werdnik",
                    Total = 10043,
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
                                Quantity = 1,
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
                    FileID = 4,
                    File = new InvoiceFile()
                    {
                        FileUri = "cdn.kwsoft.dev/invoices/abc.pdf",
                    }
                }
            };
            var expectedID = (await dbContext.Invoices.MaxAsync(i => i.ID)) + 1;

            var mockedInvoiceService = new Mock<IInvoiceService>();
            mockedInvoiceService.Setup(i => i.Create(default));

            //Act
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, mockedInvoiceService.Object);
            await repo.Add(model);
            var invoice = await repo.GetInvoceByID(expectedID.GetValueOrDefault());
            var userInvoices = await userInvoicesRepo.GetUserInvoice(model.UserID);

            //Assert
            Assert.NotNull(invoice);
            Assert.Equal(expectedID, invoice.ID);
            Assert.NotNull(userInvoices);
            Assert.Equal(3, userInvoices.Invoices.Count);
        }

        [Theory]
        [MemberData(nameof(CreateExceptionTestData))]
        public async void Add_ShouldThrowException(InvoiceCreationViewModel model, Type expectionType)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);
            var userInvoicesRepo = new UserInvoiceRepository(dbContext);

            var mockedInvoiceService = new Mock<IInvoiceService>();
            mockedInvoiceService.Setup(i => i.Create(default));

            //Act
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, mockedInvoiceService.Object);
            var uploadTask = repo.Add(model);

            //Assert
            await Assert.ThrowsAsync(expectionType, () => uploadTask);
        }

        public static object[][] CreateExceptionTestData() => new object[][]
        {
            new object[]
            {
                new InvoiceCreationViewModel()
                {
                    UserID = "kriszw",
                    Invoice = new Invoice()
                    {
                        ID = 1,
                        OrderID = 100
                    }
                },
                typeof(InvoiceAlreadyExistsWithID)
            },
            new object[]
            {
                new InvoiceCreationViewModel()
                {
                    UserID = "kriszw",
                    Invoice = new Invoice()
                    {
                        ID = default,
                        OrderID = 1
                    }
                },
                typeof(InvoiceAlreadyExistsForOrderID)
            }
        };

        [Fact]
        public async void Add_AddNewUserInvoiceModelWithNewUserID()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);
            var userInvoicesRepo = new UserInvoiceRepository(dbContext);

            var newUserID = "testForNewUSERID";

            var model = new InvoiceCreationViewModel()
            {
                UserID = newUserID,
                Invoice = new Invoice()
                {
                    OrderID = 10,
                    Address = "Görbehalom Csillés köz 1",
                    City = "Sopron",
                    FirstName = "Krisztián",
                    Country = "Hungary",
                    PostCode = "9408",
                    LastName = "Werdnik",
                    Total = 10043,
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
                                Quantity = 1,
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
                    FileID = 4,
                    File = new InvoiceFile()
                    {
                        FileUri = "cdn.kwsoft.dev/invoices/abc.pdf",
                    }
                }
            };
            var expectedID = (await dbContext.Invoices.MaxAsync(i => i.ID)) + 1;

            var mockedInvoiceService = new Mock<IInvoiceService>();
            mockedInvoiceService.Setup(i => i.Create(default));

            //Act
            var repo = new InvoiceRepository(dbContext, userInvoicesRepo, mockedInvoiceService.Object);
            await repo.Add(model);
            var invoice = await repo.GetInvoceByID(expectedID.GetValueOrDefault());
            var userInvoices = await userInvoicesRepo.GetUserInvoice(newUserID);

            //Assert
            Assert.NotNull(invoice);
            Assert.Equal(expectedID, invoice.ID);
            Assert.NotNull(userInvoices);
            Assert.Single(userInvoices.Invoices);
        }
    }
}