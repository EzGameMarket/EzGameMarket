using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CouponCode.API.Exceptions.CouponCode
{
    public class CouponCodeAlreadyUploadedException : Exception
    {
        public CouponCodeAlreadyUploadedException()
        {
        }

        public CouponCodeAlreadyUploadedException(string message) : base(message)
        {
        }

        public CouponCodeAlreadyUploadedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CouponCodeAlreadyUploadedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string CouponCode { get; set; }
    }
}
