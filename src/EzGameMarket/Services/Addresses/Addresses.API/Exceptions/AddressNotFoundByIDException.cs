using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Addresses.API.Exceptions
{
    public class AddressNotFoundByIDException : Exception
    {
        public AddressNotFoundByIDException()
        {
        }

        public AddressNotFoundByIDException(string message) : base(message)
        {
        }

        public AddressNotFoundByIDException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AddressNotFoundByIDException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
