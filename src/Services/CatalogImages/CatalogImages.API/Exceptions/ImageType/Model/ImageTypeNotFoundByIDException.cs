using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CatalogImages.API.Exceptions.ImageType.Model
{
    public class ImageTypeNotFoundByIDException : Exception
    {
        public ImageTypeNotFoundByIDException()
        {
        }

        public ImageTypeNotFoundByIDException(string message) : base(message)
        {
        }

        public ImageTypeNotFoundByIDException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ImageTypeNotFoundByIDException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
