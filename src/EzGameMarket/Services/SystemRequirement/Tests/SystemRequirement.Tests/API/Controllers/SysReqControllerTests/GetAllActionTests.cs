using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using SystemRequirement.API.Controllers;
using SystemRequirement.API.Models;
using SystemRequirement.API.Services.Repositories.Implementations;
using SystemRequirement.Tests.Fakeimplementation;
using Xunit;

namespace SystemRequirement.Tests.API.Controllers.SysReqControllerTests
{
    public class GetAllActionTests
    {
        [Fact]
        public async void GetAllSysReq_ShouldReturnSuccessAnd3SysReqs()
        {
            //Arange
            var dbContext = new FakeSysReqDbContext();
            var sysReqRepository = new SysRequirementsRepository(dbContext.DbContext);

            var expectedTotalItemsSize = 3;

            //Act
            var controller = new SysReqController(sysReqRepository);
            var actionResult = await controller.GetAllSysReq();
            var repoProduct = await sysReqRepository.GetAllSysReqAsync();

            //Assert
            Assert.NotNull(repoProduct);
            Assert.Equal(expectedTotalItemsSize,repoProduct.Count);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<List<SysReq>>>(actionResult);
            var items = Assert.IsAssignableFrom<List<SysReq>>(actionResult.Value);
            Assert.NotNull(items);
            Assert.Equal(repoProduct.Count, items.Count);
            Assert.Equal(expectedTotalItemsSize, items.Count);
        }
    }
}
