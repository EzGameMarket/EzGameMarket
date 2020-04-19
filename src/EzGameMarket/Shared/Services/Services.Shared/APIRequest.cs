using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Services.Shared.Models
{
    public class APIRequest<TModel> : IBase<TModel>
        where TModel : class
    {
        public APIRequest(TModel data) : this(Guid.NewGuid(), DateTime.Now, data)
        {
            Data = data;
        }

        public APIRequest(DateTime date, TModel data) : this(Guid.NewGuid(), date, data)
        {
            ID = Guid.NewGuid();
            CreationDate = date;
        }

        [JsonConstructor]
        public APIRequest(Guid iD, DateTime creationDate, TModel data)
        {
            ID = iD;
            CreationDate = creationDate;
            Data = data;
        }

        public Guid ID { get; private set; }

        public DateTime CreationDate { get; set; }

        public TModel Data { get; private set; }
    }
}
