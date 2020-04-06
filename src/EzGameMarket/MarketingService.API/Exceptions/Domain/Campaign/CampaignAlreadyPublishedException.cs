using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MarketingService.API.Exceptions.Domain.Campaign
{
    public class CampaignAlreadyPublishedException : Exception
    {
        public CampaignAlreadyPublishedException()
        {
        }

        public CampaignAlreadyPublishedException(string message) : base(message)
        {
        }

        public CampaignAlreadyPublishedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CampaignAlreadyPublishedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
