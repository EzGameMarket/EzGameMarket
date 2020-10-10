using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CatalogImages.API.Exceptions.Images.Model
{
    public class CImageAlreadyExistWithUrlException : Exception
    {
        public CImageAlreadyExistWithUrlException()
        {
        }

        public CImageAlreadyExistWithUrlException(string message) : base(message)
        {
        }

        public CImageAlreadyExistWithUrlException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CImageAlreadyExistWithUrlException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string ProductUri { get; set; }
    }
}
