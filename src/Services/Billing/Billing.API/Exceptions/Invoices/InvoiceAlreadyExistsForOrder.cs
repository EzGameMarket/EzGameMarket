using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Billing.API.Exceptions.Invoices
{
    public class InvoiceAlreadyExistsForOrderID : Exception
    {
        public InvoiceAlreadyExistsForOrderID()
        {
        }

        public InvoiceAlreadyExistsForOrderID(string message) : base(message)
        {
        }

        public InvoiceAlreadyExistsForOrderID(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvoiceAlreadyExistsForOrderID(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int OrderID { get; set; }
    }
}
