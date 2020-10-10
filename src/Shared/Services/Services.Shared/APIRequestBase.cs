using Newtonsoft.Json;
using Shared.Services.API.Communication.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Services.API.Communication.Models
{
    public class APIRequestBase : IBase, IRequest
    {
        public APIRequestBase() 
            : this(Guid.NewGuid(), DateTime.Now) { }

        public APIRequestBase(DateTime date) 
            : this(Guid.NewGuid(), date) { }

        [JsonConstructor]
        public APIRequestBase(Guid iD, DateTime creationDate)
        {
            ID = iD;
            CreationDate = creationDate;
        }

        public Guid ID { get; private set; }

        public DateTime CreationDate { get; private set; }
    }
}
