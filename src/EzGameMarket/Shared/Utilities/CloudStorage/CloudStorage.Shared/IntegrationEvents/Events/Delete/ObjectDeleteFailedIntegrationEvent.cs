using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Utilities.CloudStorage.Shared.IntegrationEvents.Events.Delete
{
    public class ObjectDeleteFailedIntegrationEvent : CloudStorageIntegrationEvent, IFailedIntegrationEvent
    {
        public ObjectDeleteFailedIntegrationEvent(Exception ex) : base()
        {
            Exception = ex;
        }

        public ObjectDeleteFailedIntegrationEvent(Guid id, DateTime creationDate, Exception ex) : base(id, creationDate)
        {
            Exception = ex;
        }

        public Exception Exception { get; }
    }
}
