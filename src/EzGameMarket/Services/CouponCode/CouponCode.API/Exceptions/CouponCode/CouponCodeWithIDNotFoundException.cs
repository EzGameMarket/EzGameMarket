using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CouponCode.API.Exceptions.CouponCode
{
    public class CouponCodeWithIDNotFoundException : Exception
    {
        public CouponCodeWithIDNotFoundException()
        {
        }

        public CouponCodeWithIDNotFoundException(string message) : base(message)
        {
        }

        public CouponCodeWithIDNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CouponCodeWithIDNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
