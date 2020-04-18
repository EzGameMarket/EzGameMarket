using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shared.Extensions.HttpClientHandler
{
    public interface IHttpHandlerUtil
    {
        Task<bool> SendDataWithPostAsync<TEntity>(TEntity data, string url);

        Task<TEntity> GetDataWithGetAsync<TEntity>(string url);
        Task<Stream> GetStreamWithGetAsync(string url);
        Task<byte[]> GetByteArrayWithGetAsync(string url);
    }
}
