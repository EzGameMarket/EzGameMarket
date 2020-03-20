using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Shared
{
    public interface IEventBusRepository
    {
        bool Publish<T>(T message);

        T Subscribe<T>();
    }
}
