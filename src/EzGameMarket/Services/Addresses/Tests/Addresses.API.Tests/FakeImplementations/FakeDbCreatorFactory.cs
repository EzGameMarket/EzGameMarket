using Addresses.API.Data;
using Addresses.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Addresses.Tests.FakeImplementations
{
    public static class FakeDbCreatorFactory
    {
        public static DbContextOptions CreateDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<AddressesDbContext>()
                .UseInMemoryDatabase(databaseName: $"in-memory-billing-test-{dbName}").EnableSensitiveDataLogging()
                .Options;
        }

        public static async Task InitDbContext(DbContextOptions options)
        {
            using var dbContext = new AddressesDbContext(options);

            await dbContext.AddRangeAsync(CreateModels());
            await dbContext.SaveChangesAsync();
        }

        private static List<UserAddresses> CreateModels() => new List<UserAddresses>()
        {
            new UserAddresses()
            {
                UserID = "kriszw",
                Addresses = new List<AddressModel>()
                {
                    new AddressModel()
                    {
                        ID = 1,
                        FirstName = "Werdnik"
                    },
                    new AddressModel()
                    {
                        ID = 2
                    },
                },
                DefaultAddress = new AddressModel()
                {
                    ID = 1
                }
            },
            new UserAddresses()
            {
                UserID = "test",
                Addresses = new List<AddressModel>()
                {
                    new AddressModel()
                    {
                        ID = 3
                    },
                },
                DefaultAddress = new AddressModel()
                {
                    ID = 3
                }
            }
        };
    }
}
