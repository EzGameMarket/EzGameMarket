using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CatalogImages.API.Exceptions.CatalogItemImages.Model
{
    public class CatalogItemImageNotFoundException : Exception
    {
        public CatalogItemImageNotFoundException()
        {
        }

        public CatalogItemImageNotFoundException(string message) : base(message)
        {
        }

        public CatalogItemImageNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CatalogItemImageNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int ID { get; set; }
    }
}
