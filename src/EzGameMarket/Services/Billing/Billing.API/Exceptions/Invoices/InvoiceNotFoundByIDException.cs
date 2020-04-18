using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Billing.API.Exceptions.Invoices
{
    public class InvoiceNotFoundByIDException : Exception
    {
        public InvoiceNotFoundByIDException()
        {
        }

        public InvoiceNotFoundByIDException(string message) : base(message)
        {
        }

        public InvoiceNotFoundByIDException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvoiceNotFoundByIDException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
