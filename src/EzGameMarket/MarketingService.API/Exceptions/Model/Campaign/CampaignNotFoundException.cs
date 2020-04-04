using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MarketingService.API.Exceptions.Model.Campaign
{
    public class CampaignNotFoundException : Exception
    {
        public CampaignNotFoundException()
        {
        }

        public CampaignNotFoundException(string message) : base(message)
        {
        }

        public CampaignNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CampaignNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
