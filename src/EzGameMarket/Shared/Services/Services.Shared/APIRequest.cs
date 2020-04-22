using Newtonsoft.Json;
using Shared.Services.API.Communication.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Shared.Services.API.Communication.Models
{
    public class APIRequest<TModel> : IBase<TModel>, IRequest
        where TModel : class
    {
        public APIRequest(TModel data) 
            : this(Guid.NewGuid(), DateTime.Now, data) { }

        public APIRequest(DateTime date, TModel data) 
            : this(Guid.NewGuid(), date, data) { }

        [JsonConstructor]
        public APIRequest(Guid iD, DateTime creationDate, TModel data)
        {
            ID = iD;
            CreationDate = creationDate;
            Data = data;
        }

        public Guid ID { get; private set; }

        public DateTime CreationDate { get; private set; }

        public TModel Data { get; private set; }
    }
}
