using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Shared.Utilities.EmailSender.Shared.Exceptions
{
    class EmailSendFailedException : Exception
    {
        public EmailSendFailedException()
        {
        }

        public EmailSendFailedException(string message) : base(message)
        {
        }

        public EmailSendFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmailSendFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
