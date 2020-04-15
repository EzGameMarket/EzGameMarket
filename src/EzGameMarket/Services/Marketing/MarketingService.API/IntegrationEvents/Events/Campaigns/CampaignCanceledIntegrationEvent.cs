using EventBus.Shared.Events;
using MarketingService.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.IntegrationEvents.Events.Campaigns
{
    public class CampaignCanceledIntegrationEvent : CampaignIntegrationEvent
    {
        public CampaignCanceledIntegrationEvent(Campaign campaign) : base(campaign)
        {
        }

        public CampaignCanceledIntegrationEvent(string title,
            string message,
            string couponCode,
            DateTime publishedDate,
            DateTime start,
            DateTime end,
            string shortDescription,
            string campaignImageUrl,
            int iD,
            Guid id,
            DateTime creationTime) 
            :
            base(title,
                 message,
                 couponCode,
                 publishedDate,
                 start,
                 end,
                 shortDescription,
                 campaignImageUrl,
                 iD,
                 id,
                 creationTime)
        {
        }
    }
}
