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
    public class ModifyActionTests
    {
        private SysReq GenerateUpdateModel() => new SysReq()
        {
            ID = 1,
            Type = SysReqType.Minimal,
            ProductID = "csgo",
            RAMID = 1,
            RAM = new RAMNeeds()
            {
                ID = 1,
                Size = 4,
            },
            CPUID = 1,
            CPU = new CPUNeeds()
            {
                ID = 1,
                AMDType = "Athlon 2 mag",
                IntelType = "Core I5-4200"
            },
            GPUID = 1,
            GPU = new GPUNeeds()
            {
                ID = 1,
                AMDType = "rx570",
                NVIDIAType = "gtx1060"
            },
            NetworkID = 1,
            Network = new NetworkNeeds()
            {
                ID = 1,
                Bandwidth = 100
            },
            StorageID = 1,
        };

        [Fact]
        public async void ModifySysReq_ShouldReturnSuccess()
        {
            //Arange
            var dbContext = new FakeSysReqDbContext();
            var sysReqRepository = new SysRequirementsRepository(dbContext.DbContext);
            var sysReqID = 1;

            var before = await sysReqRepository.GetSysReqAsync(sysReqID);
            before.CPU.IntelType = "Core 2 Dou";

            //Act
            var controller = new SysReqController(sysReqRepository);
            var actionResult = await controller.Update(sysReqID, before);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async void ModifySysReq_ShouldReturnNotFoundForID14()
        {
            //Arange
            var dbContext = new FakeSysReqDbContext();
            var sysReqRepository = new SysRequirementsRepository(dbContext.DbContext);
            var model = GenerateUpdateModel();
            var sysReqID = 14;
            model.ID = sysReqID;

            //Act
            var controller = new SysReqController(sysReqRepository);
            var actionResult = await controller.Update(sysReqID, model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void ModifySysReq_ShouldReturnBadRequestForIDMinus1()
        {
            //Arange
            var dbContext = new FakeSysReqDbContext();
            var sysReqRepository = new SysRequirementsRepository(dbContext.DbContext);
            var model = GenerateUpdateModel();
            var sysReqID = -1;
            model.ID = sysReqID;

            //Act
            var controller = new SysReqController(sysReqRepository);
            var actionResult = await controller.Update(sysReqID, model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void ModifySysReq_ShouldReturnBadRequestForInvalidRequest()
        {
            //Arange
            var dbContext = new FakeSysReqDbContext();
            var sysReqRepository = new SysRequirementsRepository(dbContext.DbContext);
            var model = GenerateUpdateModel();
            var sysReqID = 1;
            model.ID = sysReqID;
            model.RAM.Size = -1;

            //Act
            var controller = new SysReqController(sysReqRepository);
            controller.ModelState.AddModelError("RAMSize","A ram méréte nem lehet 0 GBnál kisebb");
            var actionResult = await controller.Update(sysReqID, model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }

    }
}
