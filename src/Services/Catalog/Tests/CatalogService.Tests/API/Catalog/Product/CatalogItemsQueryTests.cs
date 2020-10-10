using CatalogService.API.Services.Service.Implementations;
using CatalogService.Tests.API.FakeImplementation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogService.Tests.API.Catalog.Product
{
    public class CatalogItemsQueryTests
    {
        [Fact]
        public async void GetItemsFromIDsAsync_ShouldReturn2BecauseCsgoAndGtaVIsInCatalog()
        {
            var dbContext = new FakeCatalogDbContext();
            var _service = new CatalogServiceProvider(dbContext.DBContext);

            var ids = new List<string>()
            {
                "csgo",
                "gtav"
            };
            var expectedItemsSize = 2;

            var catalogItems = await _service.GetItemsFromIDsAsync(ids);

            Assert.Equal(expectedItemsSize, catalogItems.Count);
        }
        [Fact]
        public async void GetItemsFromIDsAsync_ShouldReturn1BecauseCsgoIsInCatalog()
        {
            var dbContext = new FakeCatalogDbContext();
            var _service = new CatalogServiceProvider(dbContext.DBContext);

            var ids = new List<string>()
            {
                "csgo",
            };
            var expectedItemsSize = 1;

            var catalogItems = await _service.GetItemsFromIDsAsync(ids);

            Assert.Equal(expectedItemsSize, catalogItems.Count);
        }
        [Fact]
        public async void GetItemsFromIDsAsync_ShouldReturn0BecauseCODMWIsNotInCatalog()
        {
            var dbContext = new FakeCatalogDbContext();
            var _service = new CatalogServiceProvider(dbContext.DBContext);

            var ids = new List<string>()
            {
                "codmw",
            };
            var expectedItemsSize = 0;

            var catalogItems = await _service.GetItemsFromIDsAsync(ids);

            Assert.Equal(expectedItemsSize, catalogItems.Count);
        }
    }
}
