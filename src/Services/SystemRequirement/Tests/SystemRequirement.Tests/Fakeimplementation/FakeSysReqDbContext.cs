using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemRequirement.API.Data;
using SystemRequirement.API.Models;

namespace SystemRequirement.Tests.Fakeimplementation
{
    public class FakeSysReqDbContext
    {
        public SysReqDbContext DbContext { get; set; }
        public DbContextOptions<SysReqDbContext> DbOptions { get; set; }

        public FakeSysReqDbContext()
        {
            DbOptions = new DbContextOptionsBuilder<SysReqDbContext>()
               .UseInMemoryDatabase(databaseName: $"in-memory-sysreq-test").EnableSensitiveDataLogging()
               .Options;

            var size25GB = new StorageNeeds()
            {
                ID = 1,
                Size = 25
            };
            var size50GB = new StorageNeeds()
            {
                ID = 2,
                Size = 50
            };
            var network100kb = new NetworkNeeds()
            {
                ID = 1,
                Bandwidth = 100
            };
            var network1000kb = new NetworkNeeds()
            {
                ID = 2,
                Bandwidth = 1000
            };
            var gpurx560 = new GPUNeeds()
            {
                ID = 1,
                AMDType = "rx560",
                NVIDIAType = "gtx960"
            };
            var gpugtx1080 = new GPUNeeds()
            {
                ID = 2,
                AMDType = "RX5700",
                NVIDIAType = "GTX 1080"
            };
            var cpui52200 = new CPUNeeds()
            {
                ID = 1,
                AMDType = "Athlon 2 mag",
                IntelType = "Core I5-2200"
            }; 
            var cpui77000k = new CPUNeeds()
            {
                ID = 2,
                AMDType = "Ryzen 2700X",
                IntelType = "Core I7-7700K"
            };
            var ram4gb = new RAMNeeds()
            {
                ID = 1,
                Size = 4
            };
            var ram8gb = new RAMNeeds()
            {
                ID = 2,
                Size = 8
            };
            var ram16gb = new RAMNeeds()
            {
                ID = 3,
                Size = 16
            };
            var sysReqs = new List<SysReq>()
            {
                new SysReq()
                {
                    ID = 1,
                    Type = SysReqType.Minimal,
                    ProductID = "csgo",
                    RAM = ram4gb,
                    CPU = cpui52200,
                    GPU = gpurx560,
                    Network = network100kb,
                    Storage = size25GB
                },
                new SysReq()
                {
                    ID = 2,
                    Type = SysReqType.Recommended,
                    ProductID = "bfv",
                    RAM = ram16gb,
                    CPU = cpui77000k,
                    GPU = gpugtx1080,
                    Network = network1000kb,
                    Storage = size50GB
                },
                new SysReq()
                {
                    ID = 3,
                    Type = SysReqType.Minimal,
                    ProductID = "fifa20",
                    RAM = ram8gb,
                    CPU = cpui52200,
                    GPU = gpurx560,
                    Network = network1000kb,
                    Storage = size50GB
                },
            };

            try
            {
                DbContext = new SysReqDbContext(DbOptions);
                DbContext.AddRange(sysReqs);
                DbContext.SaveChanges();
            }
            catch (Exception)
            {
                DbContext.ChangeTracker.AcceptAllChanges();
            }
        }
    }
}
