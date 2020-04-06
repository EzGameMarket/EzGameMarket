using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MarketingService.API.Exceptions.Domain.Campaign
{
    public class CampaignAlreadyDeletedException : Exception
    {
        public CampaignAlreadyDeletedException()
        {
        }

        public CampaignAlreadyDeletedException(string message) : base(message)
        {
        }

        public CampaignAlreadyDeletedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CampaignAlreadyDeletedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
