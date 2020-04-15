using Billing.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.API.Services.Repositories.Abstractions
{
    public interface IInvoiceRepository
    {
        Task<Invoice> GetInvoceByID(int id);

        Task<List<Invoice>> GetInvoices(int skip, int take);

        Task<List<Invoice>> GetInvoicesByUserID(string userID);

        Task Add(int id);
        Task Modify(int id, Invoice invoice);

        Task Storno(int id);
    }
}
