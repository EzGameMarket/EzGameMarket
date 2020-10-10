using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Addresses.API.Exceptions
{
    public class AddressAlreadyExistsWithIDException : Exception
    {
        public AddressAlreadyExistsWithIDException()
        {
        }

        public AddressAlreadyExistsWithIDException(string message) : base(message)
        {
        }

        public AddressAlreadyExistsWithIDException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AddressAlreadyExistsWithIDException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
