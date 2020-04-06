using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MarketingService.API.Exceptions.Domain.NewsletterPublisher
{
    public class InValidEmailAddressesException : Exception
    {
        public InValidEmailAddressesException()
        {
        }

        public InValidEmailAddressesException(string message) : base(message)
        {
        }

        public InValidEmailAddressesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InValidEmailAddressesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public IEnumerable<string> Emails { get; set; }
    }
}
