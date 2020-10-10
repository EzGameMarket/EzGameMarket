using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CouponCode.API.Exceptions.CouponCode
{
    public class CouponCodeNotFoundException : Exception
    {
        public CouponCodeNotFoundException()
        {
        }

        public CouponCodeNotFoundException(string message) : base(message)
        {
        }

        public CouponCodeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CouponCodeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string CouponCode { get; set; }
    }
}
