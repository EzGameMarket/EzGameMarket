using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CatalogImages.API.Exceptions.ImageSize.Model
{
    public class ImageSizeNotFoundByIDException : Exception
    {
        public ImageSizeNotFoundByIDException()
        {
        }

        public ImageSizeNotFoundByIDException(string message) : base(message)
        {
        }

        public ImageSizeNotFoundByIDException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ImageSizeNotFoundByIDException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }

    }
}
