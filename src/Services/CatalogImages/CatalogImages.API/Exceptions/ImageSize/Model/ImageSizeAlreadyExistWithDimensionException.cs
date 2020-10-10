using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CatalogImages.API.Exceptions.ImageSize.Model
{
    public class ImageSizeAlreadyExistWithDimensionException : Exception
    {
        public ImageSizeAlreadyExistWithDimensionException()
        {
        }

        public ImageSizeAlreadyExistWithDimensionException(string message) : base(message)
        {
        }

        public ImageSizeAlreadyExistWithDimensionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ImageSizeAlreadyExistWithDimensionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int Height { get; set; }
        public int Width { get; set; }
    }
}
