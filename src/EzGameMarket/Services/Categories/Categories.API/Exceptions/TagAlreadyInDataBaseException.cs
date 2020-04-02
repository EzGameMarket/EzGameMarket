using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Categories.API.Exceptions
{
    public class TagAlreadyInDataBaseException : Exception
    {
        public TagAlreadyInDataBaseException()
        {
        }

        public TagAlreadyInDataBaseException(string message) : base(message)
        {
        }

        public TagAlreadyInDataBaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TagAlreadyInDataBaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int TagID { get; set; }
        public string TagName { get; set; }
    }
}
