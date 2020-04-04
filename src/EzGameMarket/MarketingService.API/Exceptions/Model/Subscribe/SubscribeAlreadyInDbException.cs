using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MarketingService.API.Exceptions.Model.Subscribe
{
    public class SubscribeAlreadyInDbException : Exception
    {
        public SubscribeAlreadyInDbException()
        {
        }

        public SubscribeAlreadyInDbException(string message) : base(message)
        {
        }

        public SubscribeAlreadyInDbException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SubscribeAlreadyInDbException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string Email { get; set; }
    }
}
