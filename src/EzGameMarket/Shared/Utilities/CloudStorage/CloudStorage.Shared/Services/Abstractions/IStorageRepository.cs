using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.CloudStorage.Shared.Services.Abstractions
{
    public interface IStorageRepository
    {
        Task<bool> UploadFromByteArray(byte[] data);
        Task<bool> UploadFromStream(Stream stream);
        Task<bool> UploadFromByteArrayWithID(string id, byte[] data);
        Task<bool> UploadFromStreamWithID(string id, Stream stream);

        Task<byte[]> DownloadToByteArray(string id);
        Task<Stream> DownloadToStream(string id);

        Task Delete(string id);
    }
}
