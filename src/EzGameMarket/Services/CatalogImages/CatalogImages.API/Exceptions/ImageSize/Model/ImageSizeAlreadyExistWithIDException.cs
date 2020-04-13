using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CatalogImages.API.Exceptions.ImageSize.Model
{
    public class ImageSizeAlreadyExistWithIDException : Exception
    {
        public ImageSizeAlreadyExistWithIDException()
        {
        }

        public ImageSizeAlreadyExistWithIDException(string message) : base(message)
        {
        }

        public ImageSizeAlreadyExistWithIDException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ImageSizeAlreadyExistWithIDException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
