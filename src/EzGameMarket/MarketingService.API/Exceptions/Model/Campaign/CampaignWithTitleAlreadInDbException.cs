using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MarketingService.API.Exceptions.Model.Campaign
{
    public class CampaignWithTitleAlreadInDbException : Exception
    {
        public CampaignWithTitleAlreadInDbException()
        {
        }

        public CampaignWithTitleAlreadInDbException(string message) : base(message)
        {
        }

        public CampaignWithTitleAlreadInDbException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CampaignWithTitleAlreadInDbException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string Title { get; set; }
    }
}
