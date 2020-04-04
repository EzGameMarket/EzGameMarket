using MarketingService.API.Data;
using MarketingService.API.Models;
using MarketingService.API.Services.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.Services.Services.Implementations
{
    public class SubscribeManagerService : ISubscribeManagerService
    {
        private MarketingDbContext _dbContext;

        public SubscribeManagerService(MarketingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Subscribe(SubscribedMember member)
        {
            member.Active = true;
            member.SubscribedDate = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }

        public async Task UnSubscribe(SubscribedMember member)
        {
            member.Active = false;
            member.UnSubscribedDate = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }
    }
}
