using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CatalogImages.API.Exceptions.ImageType.Model
{
    public class ImageTypeAlreadyExistsWithNameException : Exception
    {
        public ImageTypeAlreadyExistsWithNameException()
        {
        }

        public ImageTypeAlreadyExistsWithNameException(string message) : base(message)
        {
        }

        public ImageTypeAlreadyExistsWithNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ImageTypeAlreadyExistsWithNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string Name { get; set; }
    }
}
