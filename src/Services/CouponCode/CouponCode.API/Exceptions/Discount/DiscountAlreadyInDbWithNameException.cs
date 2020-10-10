using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CouponCode.API.Exceptions.Discount
{
    public class DiscountAlreadyInDbWithNameException : Exception
    {
        public DiscountAlreadyInDbWithNameException()
        {
        }

        public DiscountAlreadyInDbWithNameException(string message) : base(message)
        {
        }

        public DiscountAlreadyInDbWithNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DiscountAlreadyInDbWithNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string Name { get; set; }
    }
}
