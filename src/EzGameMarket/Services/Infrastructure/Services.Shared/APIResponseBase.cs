using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Shared.Models
{
    public class APIResponseBase : IBase
    {
        [JsonConstructor]
        public APIResponseBase(Guid iD, DateTime responseCreated, DateTime requested, long executionTimeInMs, bool success)
        {
            ID = iD;
            ResponseCreated = responseCreated;
            Requested = requested;
            ExecutionTimeInMs = executionTimeInMs;
            Success = success;
        }

        public APIResponseBase(bool success, DateTime requestedDate)
            : this(Guid.NewGuid(), DateTime.Now, requestedDate, (long)(DateTime.Now - requestedDate).TotalMilliseconds, success) { }

        public Guid ID { get; private set; }

        public DateTime ResponseCreated { get; private set; }
        public DateTime Requested { get; private set; }
        public long ExecutionTimeInMs { get; private set; }
        public long ExecutionTimeInSec => ExecutionTimeInMs / 1000;

        public bool Success { get; private set; }

        public string Message { get; private set; }
    }
}
