using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemRequirement.API.Data;
using Microsoft.EntityFrameworkCore;
using SystemRequirement.API.Exceptions;
using SystemRequirement.API.Models;
using SystemRequirement.API.Services.Repositories.Abstractions;

namespace SystemRequirement.API.Services.Repositories.Implementations
{
    public class SysRequirementsRepository : ISysRequirementsRepository
    {
        private SysReqDbContext _dbContext;

        public SysRequirementsRepository(SysReqDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<SysReq>> GetAllSysReqAsync() => _dbContext.SysReqs.ToListAsync();

        public Task<SysReq> GetSysReqAsync(int sysReqID) => _dbContext.SysReqs.FirstOrDefaultAsync(s=> s.ID == sysReqID);

        public Task<List<SysReq>> GetSysReqsForProductAsync(string productID) => _dbContext.SysReqs.Where(s=> s.ProductID == productID).ToListAsync();

        public async Task Update(int sysReqID, SysReq model)
        {
            var modelToUpdate = await GetSysReqAsync(sysReqID);

            if (modelToUpdate == default)
            {
                throw new SysReqNotFoundException() { ID = sysReqID };
            }

            _dbContext.Update(model);

            await _dbContext.SaveChangesAsync();
        }

        public async Task Add(SysReq model)
        {
            if (model.ID.HasValue)
            {
                var sysReq = await GetSysReqAsync(model.ID.Value);

                if (sysReq != default)
                {
                    throw new SysReqAlreadyUploadException() { ProductID = model.ProductID, SysReqID = model.ID.Value, SysReqType = model.Type };
                } 
            }

            model.RAM = default;
            model.CPU = default;
            model.Storage = default;
            model.Network = default;
            model.GPU = default;

            await _dbContext.SysReqs.AddAsync(model);

            await _dbContext.SaveChangesAsync();
        }
    }
}
