using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using SystemRequirement.API.Models;

namespace SystemRequirement.API.Exceptions
{
    public class SysReqAlreadyUploadException : Exception
    {
        public SysReqAlreadyUploadException()
        {
        }

        public SysReqAlreadyUploadException(string message) : base(message)
        {
        }

        public SysReqAlreadyUploadException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SysReqAlreadyUploadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int SysReqID { get; set; }
        public string ProductID { get; set; }
        public SysReqType SysReqType { get; set; }
    }
}
