using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MarketingService.API.Exceptions.Domain.Campaign
{
    public class CampaignAlreadyStartedException : Exception
    {
        public CampaignAlreadyStartedException()
        {
        }

        public CampaignAlreadyStartedException(string message) : base(message)
        {
        }

        public CampaignAlreadyStartedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CampaignAlreadyStartedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
