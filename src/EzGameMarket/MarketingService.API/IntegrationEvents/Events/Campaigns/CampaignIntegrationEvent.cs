using EventBus.Shared.Events;
using MarketingService.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.IntegrationEvents.Events.Campaigns
{
    public class CampaignIntegrationEvent : IntegrationEvent
    {
        public CampaignIntegrationEvent(Campaign campaign) : base()
        {
            ID = campaign.ID.GetValueOrDefault(-1);
            Title = campaign.Title;
            ShortDescription = campaign.ShortDescription;
            Message = campaign.Description;
            CouponCode = campaign.CouponCode;
            PublishedDate = campaign.PublishedDate.GetValueOrDefault();
            Start = campaign.Start;
            End = campaign.End;
            CampaignImageUrl = campaign.CampaignImageUrl;
        }

        [JsonConstructor]
        public CampaignIntegrationEvent(string title, string message, string couponCode, DateTime publishedDate, DateTime start, DateTime end, string shortDescription, string campaignImageUrl, int iD, Guid id, DateTime creationTime) : base(id, creationTime)
        {
            Title = title;
            Message = message;
            CouponCode = couponCode;
            PublishedDate = publishedDate;
            Start = start;
            End = end;
            ShortDescription = shortDescription;
            CampaignImageUrl = campaignImageUrl;
            ID = iD;
        }

        public string Title { get; set; }

        public string Message { get; set; }

        public string CouponCode { get; set; }

        public DateTime PublishedDate { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string ShortDescription { get; set; }

        public string CampaignImageUrl { get; set; }

        public int ID { get; set; }
    }
}
