using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CouponCode.API.Exceptions.Discount
{
    public class DiscountTooHighException : Exception
    {
        public DiscountTooHighException()
        {
        }

        public DiscountTooHighException(string message) : base(message)
        {
        }

        public DiscountTooHighException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DiscountTooHighException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int Discount { get; set; }
    }
}
