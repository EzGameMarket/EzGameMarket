using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utiliies.CloudStorage.Shared.Abstractions
{
    public interface IStorageRepository
    {
        Task<bool> Upload(byte[] data);
        Task<bool> Upload(string id, byte[] data);

        Task<byte[]> Download(string id);

        Task<byte[]> Delete(string id);
    }
}
