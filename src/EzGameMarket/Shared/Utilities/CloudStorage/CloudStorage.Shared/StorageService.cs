using EventBus.Shared.Abstraction;
using Shared.Utilities.CloudStorage.Shared.Services.Abstractions;
using Shared.Utilities.CloudStorage.Shared.IntegrationEvents.Events;
using Shared.Utilities.CloudStorage.Shared.IntegrationEvents.Events.Upload;
using Shared.Utilities.CloudStorage.Shared.IntegrationEvents.Events.Download;
using Shared.Utilities.CloudStorage.Shared.IntegrationEvents.Events.Delete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventBus.Shared.Events;
using System.IO;

namespace Shared.Utilities.CloudStorage.Shared
{
    public class StorageService : IStorageService
    {
        public IStorageRepository Repository { get; private set; }

        private IEventBusRepository _eventBus;

        private bool _needToPublish = false;

        public StorageService(IStorageRepository repository, IEventBusRepository eventBus = default)
        {
            Repository = repository;
            _eventBus = eventBus;
            _needToPublish = eventBus != default;
        }

        public async Task Delete(string id)
        {
            try
            {
                await Repository.Delete(id);

                PublishToEventBus(new ObjectDeletedIntegrationEvent());
            }
            catch (Exception ex)
            {
                PublishToEventBus(new ObjectDeleteFailedIntegrationEvent(ex));

                throw;
            }
        }

        private void PublishToEventBus(IntegrationEvent @event)
        {
            if (_needToPublish)
            {
                _eventBus.Publish(@event);
            }
        }

        public Task<bool> UploadFromByteArray(byte[] data) 
            => UploadFromByteArrayWithID(Guid.NewGuid().ToString(), data);

        public Task<bool> UploadFromStream(Stream stream) 
            => UploadFromStreamWithID(Guid.NewGuid().ToString(), stream);

        public Task<bool> UploadFromByteArrayWithID(string id, byte[] data) 
            => UploadFromStreamWithID(id, new MemoryStream(data));

        public async Task<bool> UploadFromStreamWithID(string id, Stream stream)
        {
            try
            {
                var uploadResult = await Repository.UploadFromStreamWithID(id, stream);

                if (uploadResult)
                {
                    PublishToEventBus(new ObjectUploadedIntegrationEvent());
                }
                else
                {
                    PublishToEventBus(new ObjectUploadFailedIntegrationEvent(new ApplicationException($"A {id} fájl feltöltése nem sikerült")));
                }

                return uploadResult;
            }
            catch (Exception ex)
            {
                PublishToEventBus(new ObjectUploadFailedIntegrationEvent(ex));
            }

            return false;
        }

        public async Task<byte[]> DownloadToByteArray(string id)
        {
            var stream = await DownloadToStream(id);

            using var memstream = new MemoryStream();

            await stream.CopyToAsync(memstream);

            return memstream.ToArray();
        }

        public async Task<Stream> DownloadToStream(string id)
        {
            try
            {
                var stream = await Repository.DownloadToStream(id);

                PublishToEventBus(new ObjectDownloadedIntegrationEvent());

                return stream;
            }
            catch (Exception ex)
            {
                PublishToEventBus(new ObjectDownloadFailedIntegrationEvent(ex));
            }

            return default;
        }
    }
}
