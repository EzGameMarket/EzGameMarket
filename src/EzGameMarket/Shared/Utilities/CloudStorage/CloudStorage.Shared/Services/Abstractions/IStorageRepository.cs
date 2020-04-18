using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.CloudStorage.Shared.Services.Abstractions
{
    public interface IStorageRepository
    {
        Task<bool> Upload(byte[] data);
        Task<bool> Upload(Stream stream);
        Task<bool> Upload(string id, byte[] data);
        Task<bool> Upload(string id, Stream stream);
        Task<bool> UploadWithContainerExtension(string containerNameExtension, string id, Stream stream);
        Task<bool> UploadWithContainerExtension(string containerNameExtension, Stream stream);
        Task<bool> UploadWithContainerExtension(string containerNameExtension, byte[] data);
        Task<bool> UploadWithContainerExtension(string containerNameExtension, string id, byte[] data);

        Task<byte[]> DownloadToByteArray(string id);
        Task<Stream> DownloadToStream(string id);

        Task Delete(string id);
    }
}
