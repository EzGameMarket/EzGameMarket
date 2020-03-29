using CatalogService.API.Controllers;
using CatalogService.API.Services.Service.Implementations;
using CatalogService.Tests.API.FakeImplementation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CatalogService.Tests.API.Catalog.Product
{
    public class CatalogItemQueryTests
    {


        [Fact]
        public async void GetItemsAsync_ShouldReturn3CatalogItemsFrom0SkipAnd3PageSize()
        {
            var dbContext = new FakeCatalogDbContext();
            var _service = new CatalogServiceProvider(dbContext.DBContext);

            var pageIndex = 0;
            var pageSize = 3;
            var expectedItemsSize = pageSize;

            var skip = pageIndex * pageSize;
            var take = pageSize;

            var catalogItems = await _service.GetItemsAsync(skip, take);

            Assert.Equal(expectedItemsSize, catalogItems.Count);
        }
        [Fact]
        public async void GetItemsAsync_ShouldReturn2CatalogItemsFrom0SkipAnd2PageSize()
        {
            var dbContext = new FakeCatalogDbContext();
            var _service = new CatalogServiceProvider(dbContext.DBContext);

            var pageIndex = 0;
            var pageSize = 2;
            var expectedItemsSize = pageSize;

            var skip = pageIndex * pageSize;
            var take = pageSize;

            var catalogItems = await _service.GetItemsAsync(skip, take);

            Assert.Equal(expectedItemsSize,catalogItems.Count);
        }
        [Fact]
        public async void GetItemsAsync_ShouldReturn0CatalogItemsFrom1SkipAnd4PageSizeBecauseItIsOverTheNumberOfCatalogItems()
        {
            var dbContext = new FakeCatalogDbContext();
            var _service = new CatalogServiceProvider(dbContext.DBContext);

            var pageIndex = 1;
            var pageSize = 4;
            var expectedItemsSize = 0;

            var skip = pageIndex * pageSize;
            var take = pageSize;

            var catalogItems = await _service.GetItemsAsync(skip, take);

            Assert.Equal(expectedItemsSize, catalogItems.Count);
        }
        [Fact]
        public async void GetItemsAsync_ShouldReturn0CatalogItemsFrom1SkipAnd30PageSizeBecauseItIsOverTheNumberOfCatalogItems()
        {
            var dbContext = new FakeCatalogDbContext();
            var _service = new CatalogServiceProvider(dbContext.DBContext);

            var pageIndex = 1;
            var pageSize = 30;
            var expectedItemsSize = 0;

            var skip = pageIndex * pageSize;
            var take = pageSize;

            var catalogItems = await _service.GetItemsAsync(skip, take);

            Assert.Equal(expectedItemsSize, catalogItems.Count);
        }
        [Fact]
        public async void GetItemsAsync_ShouldReturn1CatalogItemsFrom1SkipAnd1PageSizeBecauseThePageSizeIs1()
        {
            var dbContext = new FakeCatalogDbContext();
            var _service = new CatalogServiceProvider(dbContext.DBContext);

            var pageIndex = 1;
            var pageSize = 1;
            var expectedItemsSize = pageSize;

            var skip = pageIndex * pageSize;
            var take = pageSize;

            var catalogItems = await _service.GetItemsAsync(skip, take);

            Assert.Equal(expectedItemsSize, catalogItems.Count);
        }
        [Fact]
        public async void GetItemsAsync_ShouldReturn1CatalogItemsFrom1SkipAnd2PageSizeBecauseThePageSizeIs1()
        {
            var dbContext = new FakeCatalogDbContext();
            var _service = new CatalogServiceProvider(dbContext.DBContext);

            var pageIndex = 1;
            var pageSize = 2;
            var expectedItemsSize = 1;

            var skip = pageIndex * pageSize;
            var take = pageSize;

            var catalogItems = await _service.GetItemsAsync(skip, take);

            Assert.Equal(expectedItemsSize, catalogItems.Count);
        }
    }
}
