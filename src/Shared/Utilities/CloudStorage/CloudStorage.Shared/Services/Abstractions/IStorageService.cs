using Shared.Utiliies.CloudStorage.Shared.Models;
using Shared.Utiliies.CloudStorage.Shared.Models.BaseResult;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.CloudStorage.Shared.Services.Abstractions
{
    public interface IStorageService
    {
        IStorageRepository Repository { get; }

        Task<CloudStorageUploadResult> Upload(byte[] data);
        Task<CloudStorageUploadResult> Upload(Stream stream);
        Task<CloudStorageUploadResult> Upload(string id, byte[] data);
        Task<CloudStorageUploadResult> Upload(string id, Stream stream);
        Task<CloudStorageUploadResult> UploadWithContainerExtension(string containerNameExtension, string id, Stream stream);
        Task<CloudStorageUploadResult> UploadWithContainerExtension(string containerNameExtension, Stream stream);
        Task<CloudStorageUploadResult> UploadWithContainerExtension(string containerNameExtension, byte[] data);
        Task<CloudStorageUploadResult> UploadWithContainerExtension(string containerNameExtension, string id, byte[] data);

        Task<CloudStorageDownloadResult> Download(string id);

        Task<CloudStorageDeleteResult> Delete(string id);
    } 
}
