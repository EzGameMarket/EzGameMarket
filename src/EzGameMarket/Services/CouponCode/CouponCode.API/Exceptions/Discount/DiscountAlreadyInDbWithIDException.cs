using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CouponCode.API.Exceptions.Discount
{
    public class DiscountAlreadyInDbWithIDException : Exception
    {
        public DiscountAlreadyInDbWithIDException()
        {
        }

        public DiscountAlreadyInDbWithIDException(string message) : base(message)
        {
        }

        public DiscountAlreadyInDbWithIDException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DiscountAlreadyInDbWithIDException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
