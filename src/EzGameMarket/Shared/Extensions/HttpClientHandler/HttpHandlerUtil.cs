using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Shared.Extensions.HttpClientHandler
{
    public class HttpHandlerUtil<TEntity> : IHttpHandlerUtil<TEntity>
    {
        private HttpClient _client;

        public HttpHandlerUtil(HttpClient client)
        {
            _client = client;
        }

        public async Task<TEntity> GetDataWithGetAsync(string url)
        {
            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var text = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TEntity>(text);
            }
            else
            {
                return default;
            }
        }

        public async Task<TEntity> GetDataWithPostAsync(string url, StringContent content)
        {
            var response = await _client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var text = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TEntity>(text);
            }
            else
            {
                return default;
            }
        }

        public async Task<bool> SendDataWithPostAsync(TEntity data, string url)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json);

            var response = await _client.PostAsync(url, content);

            return response.IsSuccessStatusCode;
        }
    }
    public class HttpHandlerUtil : IHttpHandlerUtil
    {
        private HttpClient _client;

        public HttpHandlerUtil(HttpClient client)
        {
            _client = client;
        }

        public async Task<TEntity> GetDataWithGetAsync<TEntity>(string url)
        {
            try
            {
                var response = await _client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var text = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<TEntity>(text);
                }
                else
                {
                    return default;
                }
            }
            catch (Exception)
            {
                return default;
            }
        }

        public async Task<TEntity> GetDataWithPostAsync<TEntity>(string url, StringContent content)
        {
            var response = await _client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var text = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TEntity>(text);
            }
            else
            {
                return default;
            }
        }

        public async Task<bool> SendDataWithPostAsync<TEntity>(TEntity data, string url)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json);

            var response = await _client.PostAsync(url, content);

            return response.IsSuccessStatusCode;
        }
    }
}
