using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Billing.API.Exceptions.UserInvoice
{
    public class UserInvoiceNotFoundByUserIDException : Exception
    {
        public UserInvoiceNotFoundByUserIDException()
        {
        }

        public UserInvoiceNotFoundByUserIDException(string message) : base(message)
        {
        }

        public UserInvoiceNotFoundByUserIDException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserInvoiceNotFoundByUserIDException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string UserID { get; set; }
    }
}
