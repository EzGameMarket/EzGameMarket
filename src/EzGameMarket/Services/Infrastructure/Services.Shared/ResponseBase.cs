using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Shared.Models
{
    class ResponseBase<T> : IBase<T>
        where T : class
    {
        [JsonConstructor]
        public ResponseBase(Guid iD, DateTime responseCreated, DateTime requested, long executionTimeInMs, bool success, T data)
        {
            ID = iD;
            ResponseCreated = responseCreated;
            Requested = requested;
            ExecutionTimeInMs = executionTimeInMs;
            Success = success;
            Data = data;
        }

        public ResponseBase(T data, long executionTimeMS, bool success, DateTime requestedDate)
            : this(Guid.NewGuid(), DateTime.Now, requestedDate, (long)(DateTime.Now - requestedDate).TotalMilliseconds, success, data) { }

        public Guid ID { get; private set; }

        public DateTime ResponseCreated { get; private set; }
        public DateTime Requested { get; private set; }
        public long ExecutionTimeInMs { get; private set; }
        public long ExecutionTimeInSec => ExecutionTimeInMs / 1000;

        public bool Success { get; private set; }

        public string Message { get; private set; }
        
        public T Data { get; private set; }
    }
}
