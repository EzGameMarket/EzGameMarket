using System;

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