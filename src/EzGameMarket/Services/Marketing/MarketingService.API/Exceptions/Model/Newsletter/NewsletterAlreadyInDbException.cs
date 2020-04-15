using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MarketingService.API.Exceptions.Model.Newsletter
{
    public class NewsletterAlreadyInDbException : Exception
    {
        public NewsletterAlreadyInDbException()
        {
        }

        public NewsletterAlreadyInDbException(string message) : base(message)
        {
        }

        public NewsletterAlreadyInDbException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NewsletterAlreadyInDbException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }

        public string Title { get; set; }
    }
}
