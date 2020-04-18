using Shared.Utilities.CloudStorage.AzureBlob.Settings;
using Shared.Utilities.CloudStorage.Shared.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Azure.Storage.Blobs;
using System.Threading.Tasks;
using Azure;
using Shared.Utilities.CloudStorage.Shared.Extensions;
using Shared.Extensions;

namespace Shared.Utilities.CloudStorage.AzureBlob
{
    public class AzureBlobStorage : IStorageRepository
    {
        private AzureSettings _settings;

        public AzureBlobStorage(AzureSettings settings)
        {
            _settings = settings;   
        }

        private BlobContainerClient ConnectToContainer()
            => new BlobContainerClient(_settings.ConnectionString, _settings.ContainerName);

        private BlobContainerClient ConnectToContainer(string extension) 
            => new BlobContainerClient(_settings.ConnectionString, _settings.ContainerName + extension);

        public async Task Delete(string id)
        {
            var container = ConnectToContainer();

            var blobFile = container.GetBlobClient(id);
            var deleteResult = await blobFile.DeleteIfExistsAsync();

            ValidateResponseIsErrorFree(deleteResult.GetRawResponse());

            if (deleteResult.Value == false)
            {
                throw new ApplicationException($"The delete of the {id} file failed");
            }
        }

        public async Task<byte[]> DownloadToByteArray(string id)
        {
            var stream = await DownloadToStream(id);

            return await stream.ToByteArray();
        }

        public async Task<Stream> DownloadToStream(string id)
        {
            var container = ConnectToContainer();

            var blobClient = container.GetBlobClient(id);
            var downloadResponse = await blobClient.DownloadAsync();

            ValidateResponseIsErrorFree(downloadResponse.GetRawResponse());

            return downloadResponse.Value.Content;
        }

        public Task<bool> UploadFromByteArray(byte[] data) =>
            UploadFromByteArrayWithID("".GenerateUniqueID(), data);

        public Task<bool> UploadFromByteArrayWithID(string id, byte[] data)
            => UploadFromStreamWithID(id, new MemoryStream(data));

        public Task<bool> UploadFromStream(Stream stream) =>
            UploadFromStreamWithID("".GenerateUniqueID(), stream);

        public async Task<bool> UploadFromStreamWithID(string id, Stream stream)
        {
            var container = ConnectToContainer();

            var blobClient = container.GetBlobClient(id);
            var uploadResponse = await blobClient.UploadAsync(stream);

            ValidateResponseIsErrorFree(uploadResponse.GetRawResponse());

            return true;
        }

        private void ValidateResponseIsErrorFree(Response response)
        {
            if (response.Status >= 400)
            {
                throw new ApplicationException(response.ReasonPhrase);
            }
        }

        public Task<bool> Upload(byte[] data)
        {
            using var memStream = new MemoryStream(data);

            return UploadWithContainerExtension("", "".GenerateUniqueID(), memStream);
        }

        public Task<bool> Upload(Stream stream) =>
            UploadWithContainerExtension("", "".GenerateUniqueID(), stream);

        public Task<bool> Upload(string id, byte[] data)
        {
            using var memStream = new MemoryStream(data);

            return UploadWithContainerExtension("", id, memStream);
        }

        public Task<bool> Upload(string id, Stream stream) =>
            UploadWithContainerExtension("", id, stream);

        public async Task<bool> UploadWithContainerExtension(string containerNameExtension, string id, Stream stream)
        {
            var container = ConnectToContainer(containerNameExtension);

            await container.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

            var blobClient = container.GetBlobClient(id);
            var uploadResponse = await blobClient.UploadAsync(stream);

            ValidateResponseIsErrorFree(uploadResponse.GetRawResponse());

            return true;
        }

        public Task<bool> UploadWithContainerExtension(string containerNameExtension, Stream stream)
            => UploadWithContainerExtension(containerNameExtension,"".GenerateUniqueID(),stream);

        public Task<bool> UploadWithContainerExtension(string containerNameExtension, byte[] data)
        {
            using var memStream = new MemoryStream(data);

            return UploadWithContainerExtension(containerNameExtension, "".GenerateUniqueID(), memStream);
        }

        public Task<bool> UploadWithContainerExtension(string containerNameExtension, string id, byte[] data)
        {
            using var memStream = new MemoryStream(data);

            return UploadWithContainerExtension(containerNameExtension,id, memStream);
        }
    }
}
