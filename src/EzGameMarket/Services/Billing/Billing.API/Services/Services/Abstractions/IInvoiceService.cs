using Billing.API.Models;
using Billing.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.API.Services.Services.Abstractions
{
    public interface IInvoiceService
    {
        Task UploadToBillingSystem(Invoice invoice);
        
        Task Create(InvoiceCreationViewModel model);

        Task Storno(Invoice invoice);
    }
}
