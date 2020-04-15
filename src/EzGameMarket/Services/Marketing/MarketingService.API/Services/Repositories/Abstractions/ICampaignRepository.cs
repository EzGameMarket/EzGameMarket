using MarketingService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.Services.Repositories.Abstractions
{
    public interface ICampaignRepository
    {
        Task<Campaign> Get(int id);

        Task<Campaign> GetByCampaignTitle(string name);

        Task Modify(int id, Campaign campaign);

        Task Add(Campaign campaign);

        Task<List<Campaign>> GetRunningCampaigns(DateTime startDate);
    }
}
