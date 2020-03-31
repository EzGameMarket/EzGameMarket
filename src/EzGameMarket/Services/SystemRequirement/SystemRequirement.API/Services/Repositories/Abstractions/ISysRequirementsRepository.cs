using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemRequirement.API.Models;

namespace SystemRequirement.API.Services.Repositories.Abstractions
{
    public interface ISysRequirementsRepository
    {
        Task<List<SysReq>> GetSysReqsForProductAsync(string productID);

        Task<SysReq> GetSysReqAsync(int sysReqID);

        Task<List<SysReq>> GetAllSysReqAsync();

        Task Update(int sysReqID, SysReq model);

        Task Add(SysReq model);
    }
}
