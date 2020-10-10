using System;
using System.Collections.Generic;
using System.Text;
using SystemRequirement.Tests.Fakeimplementation;
using SystemRequirement.API.Services.Repositories.Implementations;
using Xunit;
using SystemRequirement.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using SystemRequirement.API.Models;
using System.Linq;

namespace SystemRequirement.Tests.API.Controllers.SysReqControllerTests
{
    public class GetSysReqsForProductActionTests
    {
        [Fact]
        public async void GetSysReqsForProduct_ShouldReturnSuccessAnd1SysReq()
        {
            //Arange
            var dbContext = new FakeSysReqDbContext();
            var sysReqRepository = new SysRequirementsRepository(dbContext.DbContext);
            var productID = "csgo";

            var expectedItemsSize = 1;

            //Act
            var controller = new SysReqController(sysReqRepository);
            var actionResult = await controller.GetSysReqsForProductAsync(productID);
            var repoProducts = await sysReqRepository.GetSysReqsForProductAsync(productID);

            //Assert
            Assert.Equal(expectedItemsSize, repoProducts.Count);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<List<SysReq>>>(actionResult);
            var items = Assert.IsAssignableFrom<List<SysReq>>(actionResult.Value);
            Assert.NotNull(items);
            Assert.Equal(expectedItemsSize,items.Count);
        }

        [Fact]
        public async void GetSysReqsForProduct_ShouldReturnBadRequestForEmptyString()
        {
            //Arange
            var dbContext = new FakeSysReqDbContext();
            var sysReqRepository = new SysRequirementsRepository(dbContext.DbContext);
            var productID = string.Empty;

            //Act
            var controller = new SysReqController(sysReqRepository);
            var actionResult = await controller.GetSysReqsForProductAsync(productID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void GetSysReqsForProduct_ShouldReturnNotFoundForCODMW()
        {
            //Arange
            var dbContext = new FakeSysReqDbContext();
            var sysReqRepository = new SysRequirementsRepository(dbContext.DbContext);
            var productID = "codmw";

            //Act
            var controller = new SysReqController(sysReqRepository);
            var actionResult = await controller.GetSysReqsForProductAsync(productID);
            var repoProducts = await sysReqRepository.GetSysReqsForProductAsync(productID);

            //Assert
            Assert.Empty(repoProducts);
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
