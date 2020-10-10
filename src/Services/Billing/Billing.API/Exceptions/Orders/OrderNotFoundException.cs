using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Billing.API.Exceptions.Orders
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException()
        {
        }

        public OrderNotFoundException(string message) : base(message)
        {
        }

        public OrderNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OrderNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int OrderID { get; set; }
    }
}
