using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MarketingService.API.Exceptions.Model.Subscribe
{
    public class SubscriberMemberNotFoundException : Exception
    {
        public SubscriberMemberNotFoundException()
        {
        }

        public SubscriberMemberNotFoundException(string message) : base(message)
        {
        }

        public SubscriberMemberNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SubscriberMemberNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string Email { get; set; }
        public int ID { get; set; }
    }
}
