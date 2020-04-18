using Billing.API.Data;
using Billing.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Tests.FakeImplementations
{
    public static class FakeDbCreatorFactory
    {
        public static DbContextOptions CreateDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<InvoicesDbContext>()
                .UseInMemoryDatabase(databaseName: $"in-memory-billing-test-{dbName}").EnableSensitiveDataLogging()
                .Options;
        }

        public static async Task InitDbContext(DbContextOptions options)
        {
            using var dbContext = new InvoicesDbContext(options);

            await dbContext.AddRangeAsync(CreateUserInvoices());
            await dbContext.AddRangeAsync(CreateOwnData());
            await dbContext.SaveChangesAsync();
        }

        public static List<UserInvoice> CreateUserInvoices() => new List<UserInvoice>()
        {
            new UserInvoice()
            {
                UserID = "kriszw",
                Invoices = new List<Invoice>()
                {
                    new Invoice()
                    {
                        OrderID = 1,
                        Address = "Görbehalom csillés köz 1",
                        City = "Sopron",
                        FirstName = "Krisztián",
                        Country = "Hungary",
                        PostCode = "9408",
                        LastName = "Werdnik",
                        Total = 10000,
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
                                Quantity = 2,
                                ProductID = "csgo"
                            },
                            new InvoiceItem()
                            {
                                BruttoPrice = 12700,
                                NetPrice = 10000,
                                Description = "Battlefield V kulcs",
                                Name = "battlefield v",
                                Quantity = 1,
                                ProductID = "bfv"
                            }
                        },
                        FileID = 1,
                        File = new InvoiceFile()
                        {
                            FileUri = "cdn.kwsoft.dev/invoices/xyz.pdf",
                        }
                    },
                    new Invoice()
                    {
                        Address = "Görbehalom Telep 2 4a",
                        City = "Sopron",
                        FirstName = "Krisztián",
                        Country = "Hungary",
                        PostCode = "9408",
                        LastName = "Werdnik",
                        Total = 10000,
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
                                Description = "Half Life 2",
                                Name = "Half Life 2",
                                Quantity = 1,
                                ProductID = "hl2"
                            }
                        },
                        FileID = 3,
                        File = new InvoiceFile()
                        {
                            FileUri = "cdn.kwsoft.dev/invoices/testev.pdf",
                        },
                        CompanyName = "Werdnik Krisztián E.V.",
                        VATNumber = "55535966-2-28",
                        OrderID = 2
                    }
                }

            },
            new UserInvoice()
            {
                UserID = "test",
                Invoices = new List<Invoice>()
                {
                    new Invoice()
                    {
                        Address = "Test utca 1",
                        City = "Budapest",
                        FirstName = "Test",
                        Country = "Hungary",
                        PostCode = "1000",
                        LastName = "Elek",
                        Total = 10000,
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
                            }
                        },
                        FileID = 2,
                        File = new InvoiceFile()
                        {
                            FileUri = "cdn.kwsoft.dev/invoices/testelek.pdf",
                        },
                        OrderID = 5,
                    }
                }
            }
        };

        public static OwnCompanyDataModel CreateOwnData() => new OwnCompanyDataModel()
        {
            BankAccountID = 1,
            EUVATNumber = "HUxxxyxzjj",
            VATNumber = "xxxyxzjj",
            CompanyName = "Halli games",
            InvoiceBlockID = 1
        };
    }
}
