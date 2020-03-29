using CatalogService.API.Services.Service.Implementations;
using CatalogService.Tests.API.FakeImplementation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogService.Tests.API.Catalog.Filter
{
    public class FilterByCategoriesQueryTests
    {
        [Fact]
        public async void GetItemsAsync_ShouldReturn1CatalogItemsFromFPSCategory()
        {
            var dbContext = new FakeCatalogDbContext();
            var _service = new FilterService(dbContext.DBContext);

            var pageIndex = 0;
            var pageSize = 3;
            var expectedItemsSize = 1;

            var skip = pageIndex * pageSize;
            var take = pageSize;

            var categories = new List<string>()
            {
                "FPS",
            };

            var catalogItems = await _service.FilterByCategoriesAsync(skip,take,categories);

            Assert.Equal(expectedItemsSize, catalogItems.AllCount);
        }

        [Fact]
        public async void GetItemsAsync_ShouldReturn0CatalogItemsFromRPGCategory()
        {
            var dbContext = new FakeCatalogDbContext();
            var _service = new FilterService(dbContext.DBContext);

            var pageIndex = 0;
            var pageSize = 3;
            var expectedItemsSize = 0;

            var skip = pageIndex * pageSize;
            var take = pageSize;

            var categories = new List<string>()
            {
                "RPG",
            };

            var catalogItems = await _service.FilterByCategoriesAsync(skip, take, categories);

            Assert.Equal(expectedItemsSize, catalogItems.AllCount);
        }
    }
}
