using EventBus.Shared.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Utilities.Billing.Shared.IntegrationEvents.Events.Create
{
    public class BillingFailedIntegrationEvent : IntegrationEvent
    {
        public BillingFailedIntegrationEvent(Exception ex) : base()
        {
            Exception = ex;
        }

        public BillingFailedIntegrationEvent(Guid id, DateTime creationDate, Exception ex) : base(id, creationDate)
        {
            Exception = ex;
        }

        public Exception Exception { get; set; }
    }
}
