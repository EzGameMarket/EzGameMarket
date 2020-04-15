using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MarketingService.API.Exceptions.Model.Newsletter
{
    public class NewsletterNotFoundException : Exception
    {
        public NewsletterNotFoundException()
        {
        }

        public NewsletterNotFoundException(string message) : base(message)
        {
        }

        public NewsletterNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NewsletterNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }

        public string Title { get; set; }
    }
}
