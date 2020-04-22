using Billing.API.Data;
using Billing.API.Models;
using Billing.API.Services.Repositories.Abstractions;
using Billing.API.Services.Repositories.Implementations;
using Billing.API.Services.Services.Abstractions;
using Billing.API.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.API.Services.Services.Implementations
{
    public class UserInvoiceService : IUserInvoicesService
    {
        private IInvoiceRepository _invoiceRepository; 
        private InvoicesDbContext _dbContext;

        public UserInvoiceService(IInvoiceRepository invoiceRepository,
                                  InvoicesDbContext dbContext)
        {
            _invoiceRepository = invoiceRepository;
            _dbContext = dbContext;
        }

        public async Task<InvoiceFile> DownloadInvoice(int orderID)
        {
            var invoice = await _invoiceRepository.GetInvoiceForOrderID(orderID);

            return invoice != default ? invoice.File : (default);
        }

        public Task<List<Invoice>> GetInvoicesForUser(string userID) 
            => _invoiceRepository.GetInvoicesByUserID(userID);

        public Task<List<Invoice>> GetInvoicesForUser(string userID, int skip, int take) 
            => _invoiceRepository.GetInvoicesByUserIDWithSkipAndTake(userID, skip, take);

        public async Task<Invoice> GetInvoicesForUserWithOrderID(string userID, int orderID)
        {
            var userInvoices = await _dbContext.UserInvoices.Include(ui => ui.Invoices)
                .FirstOrDefaultAsync(ui => ui.UserID == userID);

            return userInvoices.Invoices.FirstOrDefault(i => i.OrderID == orderID);
        }

        public Task<IEnumerable<PaginatedInvoiceViewModel>> GetAllPaginatedInvoice(string userID) 
            => _dbContext.UserInvoices.Include(ui => ui.Invoices).Where(ui => ui.UserID == userID).Select(ui => ui.Invoices.Select(i => new PaginatedInvoiceViewModel())).FirstOrDefaultAsync();

        public async Task<PaginationViewModel<PaginatedInvoiceViewModel>> GetPaginatedInvoicesForUser(string userID, int skip, int take)
        {
            var data = await _dbContext.UserInvoices.Where(ui => ui.UserID == userID).Select(ui => ui.Invoices.Skip(skip).Take(take).Select(i => new PaginatedInvoiceViewModel())).FirstOrDefaultAsync();

            var allCountModel = await _dbContext.UserInvoices.Include(ui => ui.Invoices).FirstOrDefaultAsync(ui=> ui.UserID == userID);

            if (allCountModel != default)
            {
                return new PaginationViewModel<PaginatedInvoiceViewModel>(allCountModel.Invoices.Count, 0, 0, data);
            }
            else
            {
                return new PaginationViewModel<PaginatedInvoiceViewModel>(0, 0, 0, default);
            }
        }
    }
}
