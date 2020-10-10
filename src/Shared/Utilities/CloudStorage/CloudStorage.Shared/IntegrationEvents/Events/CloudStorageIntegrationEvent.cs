using EventBus.Shared.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Utilities.CloudStorage.Shared.IntegrationEvents.Events
{
    public class CloudStorageIntegrationEvent : IntegrationEvent
    {
        public CloudStorageIntegrationEvent() : base()
        {
        }

        public CloudStorageIntegrationEvent(Guid id, DateTime creationDate) : base(id, creationDate)
        {
        }


    }
}
