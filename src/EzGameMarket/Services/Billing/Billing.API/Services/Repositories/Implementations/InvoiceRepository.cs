using Billing.API.Data;
using Billing.API.Exceptions.Invoices;
using Billing.API.Models;
using Billing.API.Services.Repositories.Abstractions;
using Billing.API.Services.Services.Abstractions;
using Billing.API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Billing.API.Services.Repositories.Implementations
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private InvoicesDbContext _dbContext;
        private IUserInvoiceRepository _userInvoiceRepository;
        private IInvoiceService _invoiceService;

        public InvoiceRepository(InvoicesDbContext dbContext, IUserInvoiceRepository userInvoiceRepository)
        {
            _dbContext = dbContext;
            _userInvoiceRepository = userInvoiceRepository;
        }

        public async Task Add(InvoiceCreationViewModel model)
        {

            if (await AnyWithID(model.Invoice.ID.GetValueOrDefault()))
            {
                throw new InvoiceAlreadyExistsWithID() { ID = model.Invoice.ID.GetValueOrDefault() };
            }

            if (await AnyWithOrderID(model.Invoice.OrderID) == true && model.IsCanceledInvoice == false)
            {
                throw new InvoiceAlreadyExistsForOrderID() { OrderID = model.Invoice.OrderID };
            }

            var userInvoiceModel = await _userInvoiceRepository.GetUserInvoice(model.UserID);

            if (userInvoiceModel == default)
            {
                userInvoiceModel = new UserInvoice()
                {
                    UserID = model.UserID
                };

                await _userInvoiceRepository.Add(userInvoiceModel);
            }

            await _dbContext.Invoices.AddAsync(model.Invoice);
            await _dbContext.SaveChangesAsync();

            await _userInvoiceRepository.AddInvoiceForUserID(model.UserID, model.Invoice);

            await _invoiceService.Create(model);
        }

        public Task<bool> AnyWithID(int id) => _dbContext.Invoices.AnyAsync(i=> i.ID.GetValueOrDefault() == id);

        public Task<bool> AnyWithOrderID(int orderID) => _dbContext.Invoices.AnyAsync(i => i.OrderID == orderID);

        public Task<Invoice> GetInvoceByID(int id) => _dbContext.Invoices.Include(i=> i.File).Include(i=> i.Items).FirstOrDefaultAsync(i=> i.ID.GetValueOrDefault(-1) == id);

        public Task<Invoice> GetInvoiceForOrderID(int orderID) => _dbContext.Invoices.Include(i => i.File).Include(i => i.Items).FirstOrDefaultAsync(i => i.OrderID == orderID);

        public Task<List<Invoice>> GetInvoices(int skip, int take) => _dbContext.Invoices.Include(i=> i.File).Skip(skip).Take(take).ToListAsync();

        public async Task<List<Invoice>> GetInvoicesByUserID(string userID)
        {
            var userInvoice = await _dbContext.UserInvoices.Include(ui => ui.Invoices).FirstOrDefaultAsync(userInvoices => userInvoices.UserID == userID);

            return userInvoice != default && userInvoice.Invoices != default ? userInvoice.Invoices : new List<Invoice>();
        }

        public async Task<List<Invoice>> GetInvoicesByUserIDWithSkipAndTake(string userID, int skip, int take)
        {
            var userInvoice = await _dbContext.UserInvoices.Include(ui => ui.Invoices).FirstOrDefaultAsync(userInvoices => userInvoices.UserID == userID);

            return userInvoice != default ? userInvoice.Invoices.Skip(skip).Take(take).ToList() : new List<Invoice>();
        }

        public async Task Storno(int id)
        {
            var invoice = await GetInvoceByID(id);

            if (invoice != default)
            {
                invoice.SetCanceled();
                await _dbContext.SaveChangesAsync();

                await _invoiceService.Storno(invoice);
            }
            else
            {
                throw new InvoiceNotFoundByIDException() { ID = id };
            }
        }

        public async Task UpdateFilePath(string filePath, int id)
        {
            var invoice = await GetInvoceByID(id);

            if (invoice == default)
            {
                throw new InvoiceNotFoundByIDException() { ID = id };
            }

            invoice.File = new InvoiceFile()
            {
                ID = default,
                FileUri = filePath
            };

            await _dbContext.SaveChangesAsync();
        }

        public async Task UploadBillingSystemID(string billingSystemID, int id)
        {
            var invoice = await GetInvoceByID(id);

            if (invoice == default)
            {
                throw new InvoiceNotFoundByIDException() { ID = id };
            }

            invoice.BillingSystemInvoiceID = billingSystemID;

            await _dbContext.SaveChangesAsync();
        }

        public async Task UploadCanceledInvoiceBillingSystemID(string canceledID, int id)
        {
            var invoice = await GetInvoceByID(id);

            if (invoice == default)
            {
                throw new InvoiceNotFoundByIDException() { ID = id };
            }

            invoice.BillingSystemCanceledInvoiceID = canceledID;

            await _dbContext.SaveChangesAsync();
        }
    }
}