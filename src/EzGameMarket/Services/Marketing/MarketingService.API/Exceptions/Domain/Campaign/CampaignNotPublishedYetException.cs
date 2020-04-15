using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MarketingService.API.Exceptions.Domain.Campaign
{
    public class CampaignNotPublishedYetException : Exception
    {
        public CampaignNotPublishedYetException()
        {
        }

        public CampaignNotPublishedYetException(string message) : base(message)
        {
        }

        public CampaignNotPublishedYetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CampaignNotPublishedYetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
