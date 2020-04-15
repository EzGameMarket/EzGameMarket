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
            return new DbContextOptionsBuilder<BillingDbContext>()
                .UseInMemoryDatabase(databaseName: $"in-memory-billing-test-{dbName}").EnableSensitiveDataLogging()
                .Options;
        }

        public static async Task InitDbContext(DbContextOptions options)
        {
            using (var dbContext = new BillingDbContext(options))
            {

                
                await dbContext.SaveChangesAsync();
            }
        }

        public static List<Invoice> CreateInvoices() => new List<Invoice>()
        {
            new Invoice()
            {
                Address = "Görbehalom csillés köz 1",
                BillFileID = 1,
                City = "Sopron",
                FirstName = "Krisztián",
                Country = "Hungary",
                PostCode = "9408",
                LastName = "Werdnik",
                Total = 10000,
                DueDate = DateTime.Now,
                FullfiledDate = DateTime.Now,
            }
        };

        public static List<Invoice> CreateInvoiceFiles() => new List<Invoice>()
        {

        };

        public static List<Invoice> CreateInvoiceItems() => new List<Invoice>()
        {

        };

        public static List<Invoice> CreateUserInvoices() => new List<Invoice>()
        {

        };

        public static OwnCompanyDataModel CreateOwnData() => new OwnCompanyDataModel()
        {

        };
    }
}
