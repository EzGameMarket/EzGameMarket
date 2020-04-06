using EventBus.Shared.Events;
using MarketingService.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.IntegrationEvents.Events.NewsletterPublishing
{
    public class NewsletterIntegrationEvent : IntegrationEvent
    {
        public NewsletterIntegrationEvent(NewsletterMessage message) : base()
        {
            ID = message.ID.GetValueOrDefault(-1);
            Title = message.Title;
            Message = message.Message;
        }

        [JsonConstructor]
        public NewsletterIntegrationEvent(int iD, string title, string message, Guid id, DateTime creationTime) : base(id, creationTime)
        {
            ID = iD;
            Title = title;
            Message = message;
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
