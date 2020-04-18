using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Billing.API.Exceptions.Invoices
{
    public class InvoiceAlreadyExistsWithID : Exception
    {
        public InvoiceAlreadyExistsWithID()
        {
        }

        public InvoiceAlreadyExistsWithID(string message) : base(message)
        {
        }

        public InvoiceAlreadyExistsWithID(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvoiceAlreadyExistsWithID(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
