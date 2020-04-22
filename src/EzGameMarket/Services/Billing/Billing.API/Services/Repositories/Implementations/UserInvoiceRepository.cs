using Billing.API.Data;
using Billing.API.Exceptions.UserInvoice;
using Billing.API.Models;
using Billing.API.Services.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Billing.API.Services.Repositories.Implementations
{
    public class UserInvoiceRepository : IUserInvoiceRepository
    {
        private InvoicesDbContext _dbContext;

        public UserInvoiceRepository(InvoicesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(UserInvoice model)
        {
            if (await AnyUserInvoiceWithID(model.ID.GetValueOrDefault()))
            {
                throw new UserInvoiceAlreadyExistsWithIDException() { ID = model.ID.GetValueOrDefault() };
            }

            if (await AnyUserInvoiceWithUserID(model.UserID))
            {
                throw new UserInvoiceAlreadyExistsWithUserIDException() { UserID = model.UserID };
            }

            await _dbContext.UserInvoices.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddInvoiceForUserID(string userID, Invoice invoice)
        {
            var userInvoice = await GetUserInvoice(userID);

            if (userInvoice == default)
            {
                throw new UserInvoiceNotFoundByUserIDException() { UserID = userID };
            }

            if (await AnyInvoiceForUserIDWithOrderID(userID, invoice.OrderID))
            {
                throw new UserInvoiceNewInvoiceWithOrderIDForUserIDAlreadyAddedException() { UserID = userID, OrderID = invoice.OrderID };
            }

            userInvoice.Invoices.Add(invoice);

            await _dbContext.SaveChangesAsync();
        }

        private async Task<bool> AnyInvoiceForUserIDWithOrderID(string userID, int orderID) 
        {
            var userInvoice = await GetUserInvoice(userID);

            if (userInvoice != default)
            {
                return userInvoice.Invoices.Any(i=> i.OrderID == orderID);
            }
            else
            {
                throw new UserInvoiceNotFoundByUserIDException() { UserID = userID };
            }
        }

        public Task<bool> AnyUserInvoiceWithID(int id) => _dbContext.UserInvoices.AnyAsync(ui => ui.ID.GetValueOrDefault() == id);

        public Task<bool> AnyUserInvoiceWithUserID(string userID) => _dbContext.UserInvoices.AnyAsync(ui => ui.UserID == userID);

        public Task<UserInvoice> GetUserInvoice(string userID) => _dbContext.UserInvoices.Include(ui => ui.Invoices).FirstOrDefaultAsync(ui => ui.UserID == userID);
    }
}
