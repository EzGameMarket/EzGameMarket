using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Categories.API.Exceptions
{
    public class TagNotFoundException : Exception
    {
        public TagNotFoundException()
        {
        }

        public TagNotFoundException(string message) : base(message)
        {
        }

        public TagNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TagNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int TagID { get; set; }
    }
}
