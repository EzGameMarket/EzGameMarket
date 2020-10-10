using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CloudGamingSupport.API.Exceptions
{
    public class CgNotFoundException : Exception
    {
        public CgNotFoundException()
        {
        }

        public CgNotFoundException(string message) : base(message)
        {
        }

        public CgNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CgNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }

        public string ProductID { get; set; }
    }
}
