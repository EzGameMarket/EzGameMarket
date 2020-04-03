using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CloudGamingSupport.API.Exceptions
{
    public class CgAlreadyInDataBaseException : Exception
    {
        public CgAlreadyInDataBaseException()
        {
        }

        public CgAlreadyInDataBaseException(string message) : base(message)
        {
        }

        public CgAlreadyInDataBaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CgAlreadyInDataBaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        public int ID { get; set; }
        public string ProductID { get; set; }
    }
}
