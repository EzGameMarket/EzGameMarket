using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CouponCode.API.Exceptions
{
    public class CouponWithIDAlreadyUploadedException : Exception
    {
        public CouponWithIDAlreadyUploadedException()
        {
        }

        public CouponWithIDAlreadyUploadedException(string message) : base(message)
        {
        }

        public CouponWithIDAlreadyUploadedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CouponWithIDAlreadyUploadedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
