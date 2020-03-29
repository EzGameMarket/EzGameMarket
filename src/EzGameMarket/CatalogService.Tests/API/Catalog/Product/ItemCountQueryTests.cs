using System;
using System.Collections.Generic;
using System.Text;
using CatalogService.API.Services.Service.Implementations;
using CatalogService.Tests.API.FakeImplementation;
using Xunit;

namespace CatalogService.Tests.API.Catalog.Product
{
    public class ItemCountQueryTests
    {
        [Fact]
        public async void GetAllItemsCount_ShouldReturn0FromDeletedFakeCatalog()
        {
            var dbContext = new FakeCatalogDbContext();
            dbContext.DeleteCatalog();
            var _service = new CatalogServiceProvider(dbContext.DBContext);

            const long expected = 0;

            var count = await _service.GetAllItemsCount();

            Assert.Equal(expected, count);
        }
    }
}
