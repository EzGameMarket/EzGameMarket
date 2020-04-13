using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CatalogImages.API.Exceptions.ImageSize.Model
{
    public class ImageSizeAlreadyExistWithNameException : Exception
    {
        public ImageSizeAlreadyExistWithNameException()
        {
        }

        public ImageSizeAlreadyExistWithNameException(string message) : base(message)
        {
        }

        public ImageSizeAlreadyExistWithNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ImageSizeAlreadyExistWithNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string Name { get; set; }
    }
}
