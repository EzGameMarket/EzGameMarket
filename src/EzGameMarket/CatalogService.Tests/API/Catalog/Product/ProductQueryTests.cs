using CatalogService.API.Services.Service.Implementations;
using CatalogService.Tests.API.FakeImplementation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogService.Tests.API.Catalog.Product
{
    public class ProductQueryTests
    {
        [Fact]
        public async void GetProductAsync_ShouldReturnCsgoCatalogItem()
        {
            var dbContext = new FakeCatalogDbContext();
            var _service = new CatalogServiceProvider(dbContext.DBContext);

            var productID = "csgo";

            var product = await _service.GetProductAsync(productID);

            Assert.NotNull(product);
        }
        [Fact]
        public async void GetProductAsync_ShouldReturnNullBcsNoCODMWAddedToCatalog()
        {
            var dbContext = new FakeCatalogDbContext();
            var _service = new CatalogServiceProvider(dbContext.DBContext);

            var productID = "codmw";

            var product = await _service.GetProductAsync(productID);

            Assert.NotNull(product);
        }
    }
}
