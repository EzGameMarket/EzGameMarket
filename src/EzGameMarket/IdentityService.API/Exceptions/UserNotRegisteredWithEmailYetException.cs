using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace IdentityService.API.Exceptions
{
    public class UserNotRegisteredWithEmailYetException : Exception
    {
        public UserNotRegisteredWithEmailYetException()
        {
        }

        public UserNotRegisteredWithEmailYetException(string message) : base(message)
        {
        }

        public UserNotRegisteredWithEmailYetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserNotRegisteredWithEmailYetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string Email { get; set; }
    }
}
