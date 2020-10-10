using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Review.API.Exceptions
{
    public class ReviewNotFoundException : Exception
    {
        public ReviewNotFoundException()
        {
        }

        public ReviewNotFoundException(string message) : base(message)
        {
        }

        public ReviewNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ReviewNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ReviewID { get; set; }
    }
}
