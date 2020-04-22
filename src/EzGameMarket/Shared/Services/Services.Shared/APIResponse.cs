using Newtonsoft.Json;
using Shared.Services.API.Communication.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Services.API.Communication.Models
{
    public class APIResponse<TModel> : IBase<TModel>
        where TModel : class
    {
        public APIResponse(TModel data, string msg = "", bool success = true)
            : this(Guid.NewGuid(), success, msg , data) { }

        [JsonConstructor]
        public APIResponse(Guid iD, bool success, string message, TModel data)
        {
            ID = iD;
            Success = success;
            Message = message;
            Data = data;
        }

        public Guid ID { get; private set; }

        public bool Success { get; private set; }

        public string Message { get; private set; }

        public TModel Data { get; private set; }
    }
}
