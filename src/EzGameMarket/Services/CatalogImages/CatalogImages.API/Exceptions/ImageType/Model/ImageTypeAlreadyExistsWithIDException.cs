using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CatalogImages.API.Exceptions.ImageType.Model
{
    public class ImageTypeAlreadyExistsWithIDException : Exception
    {
        public ImageTypeAlreadyExistsWithIDException()
        {
        }

        public ImageTypeAlreadyExistsWithIDException(string message) : base(message)
        {
        }

        public ImageTypeAlreadyExistsWithIDException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ImageTypeAlreadyExistsWithIDException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
