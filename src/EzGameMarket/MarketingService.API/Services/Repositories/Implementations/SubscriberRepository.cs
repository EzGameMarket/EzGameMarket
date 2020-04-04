using MarketingService.API.Data;
using MarketingService.API.Models;
using MarketingService.API.Services.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.Services.Repositories.Implementations
{
    public class SubscriberRepository : ISubscriberRepository
    {
        private MarketingDbContext _dbContext;

        public SubscriberRepository(MarketingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Add(SubscribedMember model)
        {
            throw new NotImplementedException();
        }

        public Task<SubscribedMember> Get(int id) => _dbContext.Members.FirstOrDefaultAsync(m=> m.ID == id);

        public Task<List<SubscribedMember>> GetMemebersSubsribedBeetwenFromDateToDate(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public Task Modify(int id, SubscribedMember model)
        {
            throw new NotImplementedException();
        }
    }
}
