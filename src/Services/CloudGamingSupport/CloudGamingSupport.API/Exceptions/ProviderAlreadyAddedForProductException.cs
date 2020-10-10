using CloudGamingSupport.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CloudGamingSupport.API.Exceptions
{
    public class ProviderAlreadyAddedForProductException : Exception
    {
        public ProviderAlreadyAddedForProductException()
        {
        }

        public ProviderAlreadyAddedForProductException(string message) : base(message)
        {
        }

        public ProviderAlreadyAddedForProductException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProviderAlreadyAddedForProductException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ProviderModifyForGameViewModel Model { get; set; }
    }
}
