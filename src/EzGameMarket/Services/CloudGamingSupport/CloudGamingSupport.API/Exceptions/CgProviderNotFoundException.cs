using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CloudGamingSupport.API.Exceptions
{
    public class CgProviderNotFoundException : Exception
    {
        public CgProviderNotFoundException()
        {
        }

        public CgProviderNotFoundException(string message) : base(message)
        {
        }

        public CgProviderNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CgProviderNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ProviderID { get; set; }
    }
}
