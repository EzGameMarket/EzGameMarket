using EventBus.Shared.Events;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EventBus.Shared.Exceptions
{
    public class IntegrationEventPublishErrorException : Exception
    {
        public IntegrationEventPublishErrorException()
        {
        }

        public IntegrationEventPublishErrorException(string message) : base(message)
        {
        }

        public IntegrationEventPublishErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IntegrationEventPublishErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public IntegrationEvent Event { get; set; }
    }
}
