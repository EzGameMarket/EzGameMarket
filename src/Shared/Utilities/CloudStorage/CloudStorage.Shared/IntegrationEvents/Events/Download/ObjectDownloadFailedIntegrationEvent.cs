using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Utilities.CloudStorage.Shared.IntegrationEvents.Events.Download
{
    public class ObjectDownloadFailedIntegrationEvent : CloudStorageIntegrationEvent, IFailedIntegrationEvent
    {
        public ObjectDownloadFailedIntegrationEvent(Exception ex) : base()
        {
            Exception = ex;
        }

        public ObjectDownloadFailedIntegrationEvent(Guid id, DateTime creationDate, Exception ex) : base(id, creationDate)
        {
            Exception = ex;
        }

        public Exception Exception { get; }
    }
}
