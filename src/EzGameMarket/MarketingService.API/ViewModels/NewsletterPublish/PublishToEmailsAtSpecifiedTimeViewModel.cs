using MarketingService.API.ViewModels.NewsletterPublish.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.ViewModels.NewsletterPublish
{
    public class PublishToEmailsAtSpecifiedTimeViewModel : IAtSpecifiedTime, IToSpecifiedEmailAddresses, IContainsNewsletterMessageID
    {
        public PublishToEmailsAtSpecifiedTimeViewModel(IEnumerable<string> emails, DateTime time, int iD)
        {
            Emails = emails;
            Time = time;
            ID = iD;
        }

        public IEnumerable<string> Emails { get; set; }
        public DateTime Time { get; set; }

        public int ID { get; set; }
    }
}
