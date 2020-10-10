using EventBus.Shared.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Utilities.CloudStorage.Shared.IntegrationEvents.Events.Upload
{
    public class ObjectUploadFailedIntegrationEvent : CloudStorageIntegrationEvent, IFailedIntegrationEvent
    {
        public ObjectUploadFailedIntegrationEvent(Exception ex) : base()
        {

        }

        [JsonConstructor]
        public ObjectUploadFailedIntegrationEvent(Exception ex, Guid id, DateTime creationTime) : base(id, creationTime)
        {

        }

        public Exception Exception { get; }
    }
}
