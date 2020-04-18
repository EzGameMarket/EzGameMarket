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
using Shared.Utiliies.CloudStorage.Shared.Models;
using Shared.Utiliies.CloudStorage.Shared.Models.BaseResult;

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

        public async Task<CloudStorageDeleteResult> Delete(string id)
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

            return new CloudStorageDeleteResult(true);
        }

        private void PublishToEventBus(IntegrationEvent @event)
        {
            if (_needToPublish)
            {
                _eventBus.Publish(@event);
            }
        }

        public async Task<CloudStorageDownloadResult> Download(string id)
        {
            try
            {
                var response = await Repository.Download(id);

                PublishToEventBus(new ObjectDownloadedIntegrationEvent());

                return new CloudStorageDownloadResult(response.Success, response.File);
            }
            catch (Exception ex)
            {
                PublishToEventBus(new ObjectDownloadFailedIntegrationEvent(ex));
            }

            return new CloudStorageDownloadResult(false, default);
        }

        public Task<CloudStorageUploadResult> Upload(byte[] data)
        {
            using var memStream = new MemoryStream(data);

            return UploadWithContainerExtension("", "".GenerateUniqueID(), memStream);
        }

        public Task<CloudStorageUploadResult> Upload(Stream stream)
            => UploadWithContainerExtension("", "".GenerateUniqueID(), stream);

        public Task<CloudStorageUploadResult> Upload(string id, byte[] data)
        {
            using var memStream = new MemoryStream(data);

            return UploadWithContainerExtension("", id, memStream);
        }

        public Task<CloudStorageUploadResult> Upload(string id, Stream stream)
            => UploadWithContainerExtension("", id, stream);

        public async Task<CloudStorageUploadResult> UploadWithContainerExtension(string containerNameExtension, string id, Stream stream)
        {
            try
            {
                var uploadResult = await Repository.UploadWithContainerExtension(containerNameExtension,id, stream);

                if (uploadResult.Success)
                {
                    PublishToEventBus(new ObjectUploadedIntegrationEvent());
                }
                else
                {
                    PublishToEventBus(new ObjectUploadFailedIntegrationEvent(new ApplicationException($"A {id} fájl feltöltése nem sikerült")));
                }

                return new CloudStorageUploadResult(uploadResult.Success, uploadResult.AbsoluteFileURL );
            }
            catch (Exception ex)
            {
                PublishToEventBus(new ObjectUploadFailedIntegrationEvent(ex));
            }

            return new CloudStorageUploadResult(false, default);
        }

        public Task<CloudStorageUploadResult> UploadWithContainerExtension(string containerNameExtension, Stream stream)
            => UploadWithContainerExtension(containerNameExtension, "".GenerateUniqueID(), stream);

        public Task<CloudStorageUploadResult> UploadWithContainerExtension(string containerNameExtension, byte[] data)
        {
            using var memStream = new MemoryStream(data);

            return UploadWithContainerExtension(containerNameExtension, "".GenerateUniqueID(), memStream);
        }

        public Task<CloudStorageUploadResult> UploadWithContainerExtension(string containerNameExtension, string id, byte[] data)
        {
            using var memStream = new MemoryStream(data);

            return UploadWithContainerExtension(containerNameExtension,id,memStream);
        }
    }
}
