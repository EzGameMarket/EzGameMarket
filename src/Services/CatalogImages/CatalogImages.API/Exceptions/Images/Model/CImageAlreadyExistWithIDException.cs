using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CatalogImages.API.Exceptions.Images.Model
{
    public class CImageAlreadyExistWithIDException : Exception
    {
        public CImageAlreadyExistWithIDException()
        {
        }

        public CImageAlreadyExistWithIDException(string message) : base(message)
        {
        }

        public CImageAlreadyExistWithIDException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CImageAlreadyExistWithIDException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
