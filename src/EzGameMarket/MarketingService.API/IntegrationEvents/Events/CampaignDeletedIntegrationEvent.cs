using MarketingService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.IntegrationEvents.Events
{
    public class CampaignDeletedIntegrationEvent : CampaignIntegrationEvent
    {
        public CampaignDeletedIntegrationEvent(Campaign campaign) : base(campaign)
        {
        }

        public CampaignDeletedIntegrationEvent(string title, string message, string couponCode, DateTime publishedDate, DateTime start, DateTime end, string shortDescription, string campaignImageUrl, int iD, Guid id, DateTime creationTime) : base(title, message, couponCode, publishedDate, start, end, shortDescription, campaignImageUrl, iD, id, creationTime)
        {
        }
    }
}
