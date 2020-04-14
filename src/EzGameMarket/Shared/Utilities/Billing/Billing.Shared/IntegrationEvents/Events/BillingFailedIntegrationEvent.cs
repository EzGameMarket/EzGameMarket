using EventBus.Shared.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Utilities.Billing.Shared.IntegrationEvents.Events
{
    public class BillingFailedIntegrationEvent : IntegrationEvent
    {
        public BillingFailedIntegrationEvent(Exception ex) : base()
        {
        }

        public BillingFailedIntegrationEvent(Guid id, DateTime creationDate, Exception ex) : base(id, creationDate)
        {
            Exception =
        }

        public Exception Exception { get; set; }
    }
}
