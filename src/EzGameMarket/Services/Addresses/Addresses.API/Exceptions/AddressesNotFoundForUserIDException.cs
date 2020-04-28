using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Addresses.API.Exceptions
{
    public class AddressesNotFoundForUserIDException : Exception
    {
        public AddressesNotFoundForUserIDException()
        {
        }

        public AddressesNotFoundForUserIDException(string message) : base(message)
        {
        }

        public AddressesNotFoundForUserIDException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AddressesNotFoundForUserIDException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string UserID { get; set; }
    }
}
