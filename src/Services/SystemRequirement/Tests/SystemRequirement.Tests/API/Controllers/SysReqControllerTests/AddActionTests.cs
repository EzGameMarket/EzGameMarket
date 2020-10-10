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
    public class AddActionTests
    {
        private SysReq GenerateModel() => new SysReq()
        {
            ID = default,
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
        public async void AddSysReq_ShouldReturnSuccess()
        {
            //Arange
            var dbContext = new FakeSysReqDbContext();
            var sysReqRepository = new SysRequirementsRepository(dbContext.DbContext);

            var model = GenerateModel();

            //Act
            var controller = new SysReqController(sysReqRepository);
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async void AddSysReq_EntityWithID1ExitstsShouldReturnConflict()
        {
            //Arange
            var dbContext = new FakeSysReqDbContext();
            var sysReqRepository = new SysRequirementsRepository(dbContext.DbContext);

            var model = GenerateModel();
            model.ID = 1;

            //Act
            var controller = new SysReqController(sysReqRepository);
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ConflictResult>(actionResult);
        }

        [Fact]
        public async void AddSysReq_RamSizeMinusAndModelStateIsValidIsFalseShouldReturnBadRequest()
        {
            //Arange
            var dbContext = new FakeSysReqDbContext();
            var sysReqRepository = new SysRequirementsRepository(dbContext.DbContext);

            var model = GenerateModel();
            model.RAM.Size = -1;

            //Act
            var controller = new SysReqController(sysReqRepository);
            controller.ModelState.AddModelError("RAMSize", "A ram méréte nem lehet 0 GBnál kisebb");
            var actionResult = await controller.Add(model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult);
        }
    }
}
