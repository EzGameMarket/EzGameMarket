using MarketingService.API.Data;
using MarketingService.API.Exceptions.Model.Subscribe;
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

        public async Task Add(SubscribedMember model)
        {
            var member = await Get(model.ID.GetValueOrDefault(-1));

            if (member != default)
            {
                throw new SubscribeAlreadyInDbException() { Email = model.EMail };
            }

            var emailMember = await GetByEmail(model.EMail);

            if (emailMember != default)
            {
                throw new SubscribeAlreadyInDbException() { Email = model.EMail };
            }

            await _dbContext.Members.AddAsync(model);

            await _dbContext.SaveChangesAsync();
        }

        public Task<SubscribedMember> Get(int id) => _dbContext.Members.FirstOrDefaultAsync(m=> m.ID == id);

        public Task<List<SubscribedMember>> GetActiveMembers() => _dbContext.Members.Where(m=> m.Active).ToListAsync();

        public Task<List<SubscribedMember>> GetBeetwen(DateTime start, DateTime end, bool active) => _dbContext.Members.Where(m=> m.SubscribedDate >= start && m.SubscribedDate <= end && m.Active == active).ToListAsync();

        public Task<SubscribedMember> GetByEmail(string email) => _dbContext.Members.FirstOrDefaultAsync(m => m.EMail == email);

        public async Task Modify(int id, SubscribedMember model)
        {
            var member = await Get(id);

            if (member == default)
            {
                throw new SubscriberMemberNotFoundException() { Email = model.EMail, ID = id };
            }

            _dbContext.Entry(member).CurrentValues.SetValues(model);

            await _dbContext.SaveChangesAsync();
        }
    }
}
