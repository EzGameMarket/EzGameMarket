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
    public class GetActionTests
    {
        [Fact]
        public async void GetSysReq_ID1ShouldReturnSuccessAnd1SysReq()
        {
            //Arange
            var dbContext = new FakeSysReqDbContext();
            var sysReqRepository = new SysRequirementsRepository(dbContext.DbContext);
            var sysReqID = 1;

            //Act
            var controller = new SysReqController(sysReqRepository);
            var actionResult = await controller.GetSysReq(sysReqID);
            var repoProduct = await sysReqRepository.GetSysReqAsync(sysReqID);

            //Assert
            Assert.NotNull(repoProduct);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<SysReq>>(actionResult);
            var items = Assert.IsAssignableFrom<SysReq>(actionResult.Value);
            Assert.NotNull(items);
        }

        [Fact]
        public async void GetSysReq_ShouldReturnBadRequestForMinus1SysReq()
        {
            //Arange
            var dbContext = new FakeSysReqDbContext();
            var sysReqRepository = new SysRequirementsRepository(dbContext.DbContext);
            var sysReqID = -1;

            //Act
            var controller = new SysReqController(sysReqRepository);
            var actionResult = await controller.GetSysReq(sysReqID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void GetSysReq_ShouldReturnNotFoundForSysReq14()
        {
            //Arange
            var dbContext = new FakeSysReqDbContext();
            var sysReqRepository = new SysRequirementsRepository(dbContext.DbContext);
            var sysReqID = 14;

            //Act
            var controller = new SysReqController(sysReqRepository);
            var actionResult = await controller.GetSysReq(sysReqID);
            var repoProducts = await sysReqRepository.GetSysReqAsync(sysReqID);

            //Assert
            Assert.Null(repoProducts);
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
