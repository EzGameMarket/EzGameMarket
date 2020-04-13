using CatalogImages.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CatalogImages.API.Exceptions.Images.Model
{
    public class CImageTypeNotAllowedUpdateException : Exception
    {
        public CImageTypeNotAllowedUpdateException()
        {
        }

        public CImageTypeNotAllowedUpdateException(string message) : base(message)
        {
        }

        public CImageTypeNotAllowedUpdateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CImageTypeNotAllowedUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ImageID { get; set; }

        public ImageTypeModel Model { get; set; }
    }
}
