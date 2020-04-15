using MarketingService.API.ViewModels.NewsletterPublish.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.ViewModels.NewsletterPublish
{
    public class PublishToEmailsViewModel : IToSpecifiedEmailAddresses, IContainsNewsletterMessageID
    {
        public PublishToEmailsViewModel(IEnumerable<string> emails, int iD)
        {
            Emails = emails;
            ID = iD;
        }

        public IEnumerable<string> Emails { get; set; }

        public int ID { get; set; }
    }
}
