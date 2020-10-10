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
using Azure.Storage.Blobs.Models;
using Shared.Utiliies.CloudStorage.Shared.Models.AccessResult;
using System.Reflection.Metadata;

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
            => new BlobContainerClient(_settings.ConnectionString, Path.Combine(_settings.ContainerName, extension));

        public async Task<CloudStorageAccessDeleteResult> Delete(string id)
        {
            var container = ConnectToContainer();

            var blobFile = container.GetBlobClient(id);
            var deleteResult = await blobFile.DeleteIfExistsAsync();

            ValidateResponseIsErrorFree(deleteResult.GetRawResponse());

            if (deleteResult.Value == false)
            {
                throw new ApplicationException($"The delete of the {id} file failed");
            }

            return new CloudStorageAccessDeleteResult(true);
        }

        public async Task<CloudStorageAccessDownloadResult> Download(string id)
        {
            var container = ConnectToContainer();

            var blobClient = container.GetBlobClient(id);
            var downloadResponse = await blobClient.DownloadAsync();

            ValidateResponseIsErrorFree(downloadResponse.GetRawResponse());

            return new CloudStorageAccessDownloadResult(true, downloadResponse.Value.Content);
        }

        public Task<CloudStorageAccessUploadResult> UploadFromByteArray(byte[] data) =>
            UploadFromByteArrayWithID("".GenerateUniqueID(), data);

        public Task<CloudStorageAccessUploadResult> UploadFromByteArrayWithID(string id, byte[] data)
            => UploadFromStreamWithID(id, new MemoryStream(data));

        public Task<CloudStorageAccessUploadResult> UploadFromStream(Stream stream) =>
            UploadFromStreamWithID("".GenerateUniqueID(), stream);

        public async Task<CloudStorageAccessUploadResult> UploadFromStreamWithID(string id, Stream stream)
        {
            var container = ConnectToContainer();

            var blobClient = container.GetBlobClient(id);
            var uploadResponse = await blobClient.UploadAsync(stream);

            ValidateResponseIsErrorFree(uploadResponse.GetRawResponse());

            return new CloudStorageAccessUploadResult(true, blobClient.Uri.AbsoluteUri);
        }

        private void ValidateResponseIsErrorFree(Response response)
        {
            if (response.Status >= 400)
            {
                throw new ApplicationException(response.ReasonPhrase);
            }
        }

        public Task<CloudStorageAccessUploadResult> Upload(byte[] data)
        {
            using var memStream = new MemoryStream(data);

            return UploadWithContainerExtension("", "".GenerateUniqueID(), memStream);
        }

        public Task<CloudStorageAccessUploadResult> Upload(Stream stream) =>
            UploadWithContainerExtension("", "".GenerateUniqueID(), stream);

        public Task<CloudStorageAccessUploadResult> Upload(string id, byte[] data)
        {
            using var memStream = new MemoryStream(data);

            return UploadWithContainerExtension("", id, memStream);
        }

        public Task<CloudStorageAccessUploadResult> Upload(string id, Stream stream) =>
            UploadWithContainerExtension("", id, stream);

        public async Task<CloudStorageAccessUploadResult> UploadWithContainerExtension(string containerNameExtension, string id, Stream stream)
        {
            var container = ConnectToContainer(containerNameExtension);

            await container.CreateIfNotExistsAsync(PublicAccessType.Blob);

            var blobClient = container.GetBlobClient(id);
            var uploadResponse = await blobClient.UploadAsync(stream);

            ValidateResponseIsErrorFree(uploadResponse.GetRawResponse());

            return new CloudStorageAccessUploadResult(true, blobClient.Uri.AbsoluteUri);
        }

        public Task<CloudStorageAccessUploadResult> UploadWithContainerExtension(string containerNameExtension, Stream stream)
            => UploadWithContainerExtension(containerNameExtension,"".GenerateUniqueID(),stream);

        public Task<CloudStorageAccessUploadResult> UploadWithContainerExtension(string containerNameExtension, byte[] data)
        {
            using var memStream = new MemoryStream(data);

            return UploadWithContainerExtension(containerNameExtension, "".GenerateUniqueID(), memStream);
        }

        public Task<CloudStorageAccessUploadResult> UploadWithContainerExtension(string containerNameExtension, string id, byte[] data)
        {
            using var memStream = new MemoryStream(data);

            return UploadWithContainerExtension(containerNameExtension,id, memStream);
        }
    }
}
