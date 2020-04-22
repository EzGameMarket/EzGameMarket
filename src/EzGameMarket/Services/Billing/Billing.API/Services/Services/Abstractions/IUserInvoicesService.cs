using Billing.API.Models;
using Billing.API.ViewModels;
using Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.API.Services.Services.Abstractions
{
    public interface IUserInvoicesService
    {
        Task<InvoiceFile> DownloadInvoice(int orderID);

        Task<IEnumerable<PaginatedInvoiceViewModel>> GetAllPaginatedInvoice(string userID);
        Task<PaginationViewModel<PaginatedInvoiceViewModel>> GetPaginatedInvoicesForUser(string userID, int skip, int take);

        Task<List<Invoice>> GetInvoicesForUser(string userID);
        Task<List<Invoice>> GetInvoicesForUser(string userID, int skip, int take);
        Task<Invoice> GetInvoicesForUserWithOrderID(string userID, int orderID);
    }
}
