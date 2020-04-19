using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Shared.Models
{
    public class APIRequestBase : IBase
    {
        public APIRequestBase() : this(Guid.NewGuid(), DateTime.Now)
        {

        }

        public APIRequestBase(DateTime date) : this(Guid.NewGuid(), date)
        {
            ID = Guid.NewGuid();
            CreationDate = date;
        }

        [JsonConstructor]
        public APIRequestBase(Guid iD, DateTime creationDate)
        {
            ID = iD;
            CreationDate = creationDate;
        }

        public Guid ID { get; private set; }

        public DateTime CreationDate { get; set; }
    }
}
