using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CouponCode.API.Exceptions.CouponCode.CouponCodeValidation
{
    public class UserNotEligibleForCouponCodeException : Exception
    {
        public UserNotEligibleForCouponCodeException()
        {
        }

        public UserNotEligibleForCouponCodeException(string message) : base(message)
        {
        }

        public UserNotEligibleForCouponCodeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserNotEligibleForCouponCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string CouponCode { get; set; }

        public string UserID { get; set; }
    }
}
