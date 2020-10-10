using Newtonsoft.Json;
using Shared.Services.API.Communication.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Services.API.Communication.Models
{
    public class APIResponseBase : IBase, IResponse
    {
        [JsonConstructor]
        public APIResponseBase(Guid iD, bool success, string message)
        {
            ID = iD;
            Success = success;
            Message = message;
        }

        public APIResponseBase(bool success, string message)
            : this(Guid.NewGuid(), success, message) { }

        public Guid ID { get; private set; }

        public bool Success { get; private set; }

        public string Message { get; private set; }
    }
}
