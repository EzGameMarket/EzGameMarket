using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartService.API.Exceptions
{
    public class TooManyItemsException : Exception
    {
        public int ProductID { get; set; }

        public TooManyItemsException()
        {
        }

        public TooManyItemsException(string message) : base(message)
        {
        }
    }
}
