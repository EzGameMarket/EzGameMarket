using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Billing.API.Exceptions.UserInvoice
{
    public class UserInvoiceAlreadyExistsWithUserIDException : Exception
    {
        public UserInvoiceAlreadyExistsWithUserIDException()
        {
        }

        public UserInvoiceAlreadyExistsWithUserIDException(string message) : base(message)
        {
        }

        public UserInvoiceAlreadyExistsWithUserIDException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserInvoiceAlreadyExistsWithUserIDException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string UserID { get; set; }
    }
}
