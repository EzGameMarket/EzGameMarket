using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Addresses.API.Exceptions
{
    public class AddressNotAsignedForUserIDException : Exception
    {
        public AddressNotAsignedForUserIDException()
        {
        }

        public AddressNotAsignedForUserIDException(string message) : base(message)
        {
        }

        public AddressNotAsignedForUserIDException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AddressNotAsignedForUserIDException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int AddressID { get; set; }
        public string UserID { get; set; }
    }
}
