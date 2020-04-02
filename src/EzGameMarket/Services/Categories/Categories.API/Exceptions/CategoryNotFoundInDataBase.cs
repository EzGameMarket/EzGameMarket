using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Categories.API.Exceptions
{
    public class CategoryNotFoundInDataBaseException : Exception
    {
        public CategoryNotFoundInDataBaseException()
        {
        }

        public CategoryNotFoundInDataBaseException(string message) : base(message)
        {
        }

        public CategoryNotFoundInDataBaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CategoryNotFoundInDataBaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int CategoryID { get; set; }
    }
}
