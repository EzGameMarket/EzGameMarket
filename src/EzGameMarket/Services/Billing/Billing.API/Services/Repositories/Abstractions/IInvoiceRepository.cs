using Billing.API.Controllers;
using Billing.API.Models;
using Billing.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.API.Services.Repositories.Abstractions
{
    public interface IInvoiceRepository
    {
        Task<Invoice> GetInvoceByID(int id);

        Task<Invoice> GetInvoiceForOrderID(int orderID);

        Task<List<Invoice>> GetInvoices(int skip, int take);

        Task<List<Invoice>> GetInvoicesByUserID(string userID);
        Task<List<Invoice>> GetInvoicesByUserIDWithSkipAndTake(string userID, int skip, int take);

        Task<bool> AnyWithID(int id);
        Task<bool> AnyWithOrderID(int orderID);

        Task Add(InvoiceCreationViewModel model);

        Task Storno(int id);

        Task UploadBillingSystemID(string billingSystemID);
    }
}
