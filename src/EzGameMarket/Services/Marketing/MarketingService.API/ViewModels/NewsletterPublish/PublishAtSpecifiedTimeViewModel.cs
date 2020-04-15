using MarketingService.API.ViewModels.NewsletterPublish.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.ViewModels.NewsletterPublish
{
    public class PublishAtSpecifiedTimeViewModel : IAtSpecifiedTime, IContainsNewsletterMessageID
    {
        public PublishAtSpecifiedTimeViewModel(DateTime time, int iD)
        {
            Time = time;
            ID = iD;
        }

        public DateTime Time { get; set; }

        public int ID { get; set; }
    }
}
