using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shared.Extensions.HttpClientHandler
{
    public interface IHttpHandlerUtil<TEntity>
    {
        Task<bool> SendDataWithPostAsync(TEntity data, string url);

        Task<TEntity> GetDataWithPostAsync(string url, StringContent content);
        Task<TEntity> GetDataWithGetAsync(string url);
    }
    public interface IHttpHandlerUtil
    {
        Task<bool> SendDataWithPostAsync<TEntity>(TEntity data, string url);

        Task<TEntity> GetDataWithPostAsync<TEntity>(string url, StringContent content);
        Task<TEntity> GetDataWithGetAsync<TEntity>(string url);
    }
}
