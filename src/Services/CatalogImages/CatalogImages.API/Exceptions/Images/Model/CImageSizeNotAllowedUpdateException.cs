using CatalogImages.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CatalogImages.API.Exceptions.Images.Model
{
    public class CImageSizeNotAllowedUpdateException : Exception
    {
        public CImageSizeNotAllowedUpdateException()
        {
        }

        public CImageSizeNotAllowedUpdateException(string message) : base(message)
        {
        }

        public CImageSizeNotAllowedUpdateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CImageSizeNotAllowedUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ImageID { get; set; }

        public ImageSizeModel Model { get; set; }
    }
}
