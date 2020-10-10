using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Categories.API.Exceptions
{
    public class CategoryAlreadyInDataBaseException : Exception
    {
        public CategoryAlreadyInDataBaseException()
        {
        }

        public CategoryAlreadyInDataBaseException(string message) : base(message)
        {
        }

        public CategoryAlreadyInDataBaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CategoryAlreadyInDataBaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
