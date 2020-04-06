using EventBus.Shared.Abstraction;
using EventBus.Shared.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.MockedTest
{
    public class MagicBus : IEventBusRepository, IDisposable
    {

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MagicBus()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        public async void Publish(IntegrationEvent @event)
        {
            Console.WriteLine($"{@event.Id} Event publishing...");

            await Task.Delay(100);

            Console.WriteLine($"{@event.Id} Event published");
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            Console.WriteLine($"{nameof(T)} Event subscribed to {nameof(TH)}");
        }

        public void SubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            Console.WriteLine($"{eventName} Event subscribed to {nameof(TH)}");
        }

        public void UnsubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            Console.WriteLine($"{eventName} Event unsubscribed to {nameof(TH)}");
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            Console.WriteLine($"{nameof(T)} Event unsubscribed to {nameof(TH)}");
        }
        #endregion
    }
}
