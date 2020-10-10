using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CatalogImages.API.Exceptions.Images.Model
{
    public class CImageNotFoundException : Exception
    {
        public CImageNotFoundException()
        {
        }

        public CImageNotFoundException(string message) : base(message)
        {
        }

        public CImageNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CImageNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
