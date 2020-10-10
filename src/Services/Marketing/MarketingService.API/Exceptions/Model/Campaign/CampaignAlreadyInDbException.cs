using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MarketingService.API.Exceptions.Model.Campaign
{
    public class CampaignAlreadyInDbException : Exception
    {
        public CampaignAlreadyInDbException()
        {
        }

        public CampaignAlreadyInDbException(string message) : base(message)
        {
        }

        public CampaignAlreadyInDbException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CampaignAlreadyInDbException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }

        public string Title { get; set; }
    }
}
