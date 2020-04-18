using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.Billing.Shared.Services.Abstractions
{
    public interface IBillingAPIFileManager
    {
        Task<Stream> DownloadInvoice(string id); 
    }
}
