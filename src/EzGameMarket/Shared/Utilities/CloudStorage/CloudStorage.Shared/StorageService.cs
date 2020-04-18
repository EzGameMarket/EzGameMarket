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
using Shared.Utilities.CloudStorage.Shared.Extensions;
using Shared.Extensions;

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

        public async Task<byte[]> DownloadToByteArray(string id)
        {
            var stream = await DownloadToStream(id);

            return await stream.ToByteArray();
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

        public Task<bool> Upload(byte[] data)
        {
            using var memStream = new MemoryStream(data);

            return UploadWithContainerExtension("", "".GenerateUniqueID(), memStream);
        }

        public Task<bool> Upload(Stream stream)
            => UploadWithContainerExtension("", "".GenerateUniqueID(), stream);

        public Task<bool> Upload(string id, byte[] data)
        {
            using var memStream = new MemoryStream(data);

            return UploadWithContainerExtension("", id, memStream);
        }

        public Task<bool> Upload(string id, Stream stream)
            => UploadWithContainerExtension("", id, stream);

        public async Task<bool> UploadWithContainerExtension(string containerNameExtension, string id, Stream stream)
        {
            try
            {
                var uploadResult = await Repository.UploadWithContainerExtension(containerNameExtension,id, stream);

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

        public Task<bool> UploadWithContainerExtension(string containerNameExtension, Stream stream)
            => UploadWithContainerExtension(containerNameExtension, "".GenerateUniqueID(), stream);

        public Task<bool> UploadWithContainerExtension(string containerNameExtension, byte[] data)
        {
            using var memStream = new MemoryStream(data);

            return UploadWithContainerExtension(containerNameExtension, "".GenerateUniqueID(), memStream);
        }

        public Task<bool> UploadWithContainerExtension(string containerNameExtension, string id, byte[] data)
        {
            using var memStream = new MemoryStream(data);

            return UploadWithContainerExtension(containerNameExtension,id,memStream);
        }
    }
}
