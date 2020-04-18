using Shared.Extensions.HttpClientHandler;
using Shared.Utilities.Billing.Shared.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.Billing.Billingo.Services.Implementations
{
    public class BillingoAPIFileManager : IBillingAPIFileManager
    {
        private IHttpHandlerUtil _httpHandlerUtil;

        private const string apiEndPoint = "https://www.billingo.hu/api/invoices/{id}/download";

        public BillingoAPIFileManager(IHttpHandlerUtil httpHandlerUtil)
        {
            _httpHandlerUtil = httpHandlerUtil;
        }

        public Task<Stream> DownloadInvoice(string id) 
            => _httpHandlerUtil.GetStreamWithGetAsync(apiEndPoint.Replace("{id}", id));
    }
}
