using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Billing.API.Exceptions.UserInvoice
{
    public class UserInvoiceAlreadyExistsWithIDException : Exception
    {
        public UserInvoiceAlreadyExistsWithIDException()
        {
        }

        public UserInvoiceAlreadyExistsWithIDException(string message) : base(message)
        {
        }

        public UserInvoiceAlreadyExistsWithIDException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserInvoiceAlreadyExistsWithIDException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
