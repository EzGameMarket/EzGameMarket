using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Billing.API.Exceptions.UserInvoice
{
    public class UserInvoiceNewInvoiceWithOrderIDForUserIDAlreadyAddedException : Exception
    {
        public UserInvoiceNewInvoiceWithOrderIDForUserIDAlreadyAddedException()
        {
        }

        public UserInvoiceNewInvoiceWithOrderIDForUserIDAlreadyAddedException(string message) : base(message)
        {
        }

        public UserInvoiceNewInvoiceWithOrderIDForUserIDAlreadyAddedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserInvoiceNewInvoiceWithOrderIDForUserIDAlreadyAddedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int OrderID { get; set; }
        public string UserID { get; set; }
    }
}
