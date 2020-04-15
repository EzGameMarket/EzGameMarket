using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MarketingService.API.Exceptions.Domain.Campaign
{
    public class CampaignAlreadyCanceledException : Exception
    {
        public CampaignAlreadyCanceledException()
        {
        }

        public CampaignAlreadyCanceledException(string message) : base(message)
        {
        }

        public CampaignAlreadyCanceledException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CampaignAlreadyCanceledException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
