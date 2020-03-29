using CatalogService.API.Controllers;
using CatalogService.API.ViewModels.Products;
using CatalogService.Tests.API.FakeImplementation;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Pagination;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Net;
using System.Diagnostics;
using CatalogService.API.Services.Service.Implementations;

namespace CatalogService.Tests.API.Catalog.Controllers.Catalog
{
    public class CatalogControllerTests
    {
        [Fact]
        public async void Get_ItemDetial_Success()
        {
            var dbContext = new FakeCatalogDbContext();
            var catalogService = new CatalogServiceProvider(dbContext.DBContext);

            var productID = "1";
            var expectedAllProduct = 3;

            var controller = new CatalogController(catalogService);
            var actionResult = await controller.GetProductDetail(productID);

            //Assert
            Assert.Equal(expectedAllProduct, await catalogService.GetAllItemsCount());
            Assert.IsType<ActionResult<ProductViewModel>>(actionResult);
            var product = Assert.IsAssignableFrom<ProductViewModel>(actionResult.Value);
            Assert.NotNull(product);
        }
        [Fact]
        public async void Get_ItemsPaginated_ShouldReturnSuccessAnd3Items()
        {
            var dbContext = new FakeCatalogDbContext();
            var catalogService = new CatalogServiceProvider(dbContext.DBContext);

            var pageIndex = 0;
            var pageSize = 30;

            var expectedItemsCount = 3;
            var expectedPageSize = 30;
            var expectedPageIndex = 0;

            var controller = new CatalogController(catalogService);
            var actionResult = await controller.GetItems(pageIndex, pageSize);

            //Assert
            Assert.IsType<ActionResult<PaginationViewModel<CatalogItem>>>(actionResult);
            Assert.NotNull(actionResult.Value);
            var products = Assert.IsAssignableFrom<PaginationViewModel<CatalogItem>>(actionResult.Value);
            Assert.Equal(expectedItemsCount, products.TotalItemsCount);
            Assert.Equal(expectedItemsCount, products.Data.Count());
            Assert.Equal(expectedPageSize, products.ItemsPerPage);
            Assert.Equal(expectedPageIndex, products.ActualPage);
        }
        [Fact]
        public async void Get_ItemsFromIDs_ShouldReturnSuccessAnd2Items()
        {
            var dbContext = new FakeCatalogDbContext();
            var catalogService = new CatalogServiceProvider(dbContext.DBContext);

            var ids = new List<string>()
            {
                "csgo",
                "bfv"
            };

            var expectedItemsCount = 2;

            var controller = new CatalogController(catalogService);
            var actionResult = await controller.GetItemsFromIDS(ids);

            //Assert
            Assert.IsType<ActionResult<List<CatalogItem>>>(actionResult);
            Assert.NotNull(actionResult.Value);
            var products = Assert.IsAssignableFrom<List<CatalogItem>>(actionResult.Value);
            Assert.Equal(expectedItemsCount, products.Count);
        }
    }
}
