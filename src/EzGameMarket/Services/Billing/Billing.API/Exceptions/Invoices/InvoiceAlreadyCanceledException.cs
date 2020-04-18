using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Billing.API.Exceptions.Invoices
{
    public class InvoiceAlreadyCanceledException : Exception
    {
        public InvoiceAlreadyCanceledException()
        {
        }

        public InvoiceAlreadyCanceledException(string message) : base(message)
        {
        }

        public InvoiceAlreadyCanceledException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvoiceAlreadyCanceledException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int InvoiceID { get; set; }

        public int OrderID { get; set; }
    }
}
