using CloudGamingSupport.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CloudGamingSupport.API.Exceptions
{
    public class ProviderDoesNotAddedToTheGameYetException : Exception
    {
        public ProviderDoesNotAddedToTheGameYetException()
        {
        }

        public ProviderDoesNotAddedToTheGameYetException(string message) : base(message)
        {
        }

        public ProviderDoesNotAddedToTheGameYetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProviderDoesNotAddedToTheGameYetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ProviderModifyForGameViewModel Model { get; set; }
    }
}
