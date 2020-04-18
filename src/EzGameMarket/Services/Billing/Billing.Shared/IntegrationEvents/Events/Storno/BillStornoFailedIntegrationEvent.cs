using EventBus.Shared.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Billing.Shared.IntegrationEvents.Events.Storno
{
    class BillStornoFailedIntegrationEvent : IntegrationEvent
    {
        public BillStornoFailedIntegrationEvent(Exception ex) : base()
        {
            Exception = ex;
        }

        public BillStornoFailedIntegrationEvent(Guid id, DateTime creationDate, Exception ex) : base(id, creationDate)
        {
            Exception = ex;
        }

        public Exception Exception { get; set; }
    }
}
