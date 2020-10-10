using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CouponCode.API.Exceptions.CouponCode.CouponCodeValidation
{
    public class CouponCodeOutdatedException : Exception
    {
        public CouponCodeOutdatedException()
        {
        }

        public CouponCodeOutdatedException(string message) : base(message)
        {
        }

        public CouponCodeOutdatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CouponCodeOutdatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string CouponCode { get; set; }

        public DateTime ValidToDate { get; set; }
    }
}
