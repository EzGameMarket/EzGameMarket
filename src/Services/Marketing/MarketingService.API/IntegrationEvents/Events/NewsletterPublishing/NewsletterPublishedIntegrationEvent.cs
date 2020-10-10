using MarketingService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.IntegrationEvents.Events.NewsletterPublishing
{
    public class NewsletterPublishedIntegrationEvent : NewsletterIntegrationEvent
    {
        public NewsletterPublishedIntegrationEvent(NewsletterMessage message, IEnumerable<string> emails) : base(message)
        {
            Emails = emails;
        }

        public NewsletterPublishedIntegrationEvent(int iD, string title, string message, IEnumerable<string> emails, Guid id, DateTime creationTime) : base(iD, title, message, id, creationTime)
        {
            Emails = emails;
        }

        public IEnumerable<string> Emails { get; set; }
    }
}
