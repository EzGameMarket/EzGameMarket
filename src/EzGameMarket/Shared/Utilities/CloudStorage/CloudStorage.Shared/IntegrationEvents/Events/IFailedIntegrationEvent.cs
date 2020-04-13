using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Utilities.CloudStorage.Shared.IntegrationEvents.Events
{
    public interface IFailedIntegrationEvent
    {
        Exception Exception { get; }
    }
}
