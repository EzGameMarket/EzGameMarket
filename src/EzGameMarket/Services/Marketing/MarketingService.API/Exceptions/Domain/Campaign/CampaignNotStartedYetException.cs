using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MarketingService.API.Exceptions.Domain.Campaign
{
    public class CampaignNotStartedYetException : Exception
    {
        public CampaignNotStartedYetException()
        {
        }

        public CampaignNotStartedYetException(string message) : base(message)
        {
        }

        public CampaignNotStartedYetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CampaignNotStartedYetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
