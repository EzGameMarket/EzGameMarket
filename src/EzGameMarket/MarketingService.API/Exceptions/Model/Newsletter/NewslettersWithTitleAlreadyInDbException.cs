using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MarketingService.API.Exceptions.Model.Newsletter
{
    public class NewslettersWithTitleAlreadyInDbException : Exception
    {
        public NewslettersWithTitleAlreadyInDbException()
        {
        }

        public NewslettersWithTitleAlreadyInDbException(string message) : base(message)
        {
        }

        public NewslettersWithTitleAlreadyInDbException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NewslettersWithTitleAlreadyInDbException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string Title { get; set; }
    }
}
