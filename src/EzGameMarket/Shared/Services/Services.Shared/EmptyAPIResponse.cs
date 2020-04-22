using Newtonsoft.Json;
using Shared.Services.API.Communication.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Services.API.Communication.Models
{
    public class EmptyAPIResponse : IBase, IResponse
    {
        public EmptyAPIResponse(string msg, bool success = false) 
            : this(success, msg) { }

        public EmptyAPIResponse(bool success = true, string message = "") 
            : this(Guid.NewGuid(), success, message) { }

        [JsonConstructor]
        public EmptyAPIResponse(Guid iD, bool success, string message)
        {
            ID = iD;
            Success = success;
            Message = message;
        }

        public Guid ID { get; set; }

        public bool Success { get; }

        public string Message { get; }
    }
}
