using MarketingService.API.Data;
using MarketingService.API.Exceptions.Model.Campaign;
using MarketingService.API.Models;
using MarketingService.API.Services.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.Services.Repositories.Implementations
{
    public class CampaignRepository : ICampaignRepository
    {
        private MarketingDbContext _dbContext;

        public CampaignRepository(MarketingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Campaign model)
        {
            var campaign = await Get(model.ID.GetValueOrDefault(-1));

            if (campaign != default)
            {
                throw new CampaignAlreadyInDbException() { ID = model.ID.GetValueOrDefault(-1), Title = model.Title };
            }

            var campaignFromTitle = await GetByCampaignTitle(model.Title);

            if (campaignFromTitle != default)
            {
                throw new CampaignWithTitleAlreadInDbException() { Title = model.Title };
            }

            await _dbContext.Campaigns.AddAsync(model);

            await _dbContext.SaveChangesAsync();
        }

        public Task<Campaign> Get(int id) => _dbContext.Campaigns.FirstOrDefaultAsync(c=> c.ID == id);

        public Task<Campaign> GetByCampaignTitle(string name) => _dbContext.Campaigns.FirstOrDefaultAsync(c=> c.Title == name);

        public async Task Modify(int id, Campaign model)
        {
            var campaign = await Get(id);

            if (campaign == default)
            {
                throw new CampaignNotFoundException() { ID = id };
            }

            _dbContext.Entry(campaign).CurrentValues.SetValues(model);

            await _dbContext.SaveChangesAsync();
        }

        public Task<List<Campaign>> GetRunningCampaigns(DateTime startDate) => _dbContext.Campaigns.Where(c => c.Start >= startDate && c.End >= DateTime.Now).ToListAsync();
    }
}
