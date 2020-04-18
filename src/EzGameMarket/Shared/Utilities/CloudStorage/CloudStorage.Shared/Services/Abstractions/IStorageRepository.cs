using Shared.Utiliies.CloudStorage.Shared.Models.AccessResult;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.CloudStorage.Shared.Services.Abstractions
{
    public interface IStorageRepository
    {
        Task<CloudStorageAccessUploadResult> Upload(byte[] data);
        Task<CloudStorageAccessUploadResult> Upload(Stream stream);
        Task<CloudStorageAccessUploadResult> Upload(string id, byte[] data);
        Task<CloudStorageAccessUploadResult> Upload(string id, Stream stream);
        Task<CloudStorageAccessUploadResult> UploadWithContainerExtension(string containerNameExtension, string id, Stream stream);
        Task<CloudStorageAccessUploadResult> UploadWithContainerExtension(string containerNameExtension, Stream stream);
        Task<CloudStorageAccessUploadResult> UploadWithContainerExtension(string containerNameExtension, byte[] data);
        Task<CloudStorageAccessUploadResult> UploadWithContainerExtension(string containerNameExtension, string id, byte[] data);

        Task<CloudStorageAccessDownloadResult> Download(string id);

        Task<CloudStorageAccessDeleteResult> Delete(string id);
    }
}
