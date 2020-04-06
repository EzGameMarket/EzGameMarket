using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.ViewModels.NewsletterPublish.Abstractions
{
    public interface IContainsNewsletterMessageID
    {
        public int ID { get; set; }
    }
}
