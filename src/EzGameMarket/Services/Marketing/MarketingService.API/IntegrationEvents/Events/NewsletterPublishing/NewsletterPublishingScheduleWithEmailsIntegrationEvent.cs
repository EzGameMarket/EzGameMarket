using MarketingService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.IntegrationEvents.Events.NewsletterPublishing
{
    public class NewsletterPublishingScheduleWithEmailsIntegrationEvent : NewsletterIntegrationEvent
    {
        public NewsletterPublishingScheduleWithEmailsIntegrationEvent(NewsletterMessage message, DateTime date, IEnumerable<string> emails) : base(message)
        {
            Time = date;
            Emails = emails;
        }

        public NewsletterPublishingScheduleWithEmailsIntegrationEvent(int iD, string title, string message, Guid id, DateTime creationTime, DateTime date, IEnumerable<string> emails) : base(iD, title, message, id, creationTime)
        {
            Time = date;
            Emails = emails;
        }

        public DateTime Time { get; set; }

        public IEnumerable<string> Emails { get; set; }
    }
}
