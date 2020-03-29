using CatalogService.API.Controllers;
using CatalogService.API.Services.Service.Implementations;
using CatalogService.API.ViewModels.Products;
using CatalogService.Tests.API.FakeImplementation;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CatalogService.Tests.API.Catalog.Controllers.Filter
{
    public class FilterControllerTests
    {

        [Fact]
        public async void Get_ItemsFromIDs_ShouldReturnBadRequestAnd0Items()
        {
            var dbContext = new FakeCatalogDbContext();
            var filterService = new FilterService(dbContext.DBContext);

            var pageIndex = 0;
            var pageSize = 30;

            var controller = new FilterController(filterService);
            var actionResult = await controller.GetFilteredItems(pageIndex, pageSize);

            //Assert
            Assert.IsType<BadRequestResult>(actionResult.Result);
            Assert.Null(actionResult.Value);
        }
    }
}
