using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SystemRequirement.API.Exceptions
{
    public class SysReqNotFoundException : Exception
    {
        public SysReqNotFoundException()
        {
        }

        public SysReqNotFoundException(string message) : base(message)
        {
        }

        public SysReqNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SysReqNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
