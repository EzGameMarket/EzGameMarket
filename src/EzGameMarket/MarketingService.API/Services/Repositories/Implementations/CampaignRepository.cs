using MarketingService.API.Models;
using MarketingService.API.Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.Services.Repositories.Implementations
{
    public class CampaignRepository : ICampaignRepository
    {
        public Task Add(Campaign campaign)
        {
            throw new NotImplementedException();
        }

        public Task<Campaign> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Campaign> GetByCampaignTitle(string name)
        {
            throw new NotImplementedException();
        }

        public Task GetRunningCampaigns(DateTime startDate)
        {
            throw new NotImplementedException();
        }

        public Task Modify(int id, Campaign campaign)
        {
            throw new NotImplementedException();
        }

        Task<List<Campaign>> ICampaignRepository.GetRunningCampaigns(DateTime startDate)
        {
            throw new NotImplementedException();
        }
    }
}
