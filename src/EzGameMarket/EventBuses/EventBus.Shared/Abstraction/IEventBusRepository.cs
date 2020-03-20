using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Shared.Abstraction
{
    public interface IEventBusRepository
    {
        bool Publish<T>(T message);

        void Subscribe<TEvent,TH>();

        void UnSubscribe<TEvent>();
    }
}
