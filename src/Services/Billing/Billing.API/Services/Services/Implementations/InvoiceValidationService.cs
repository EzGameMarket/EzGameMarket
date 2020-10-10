using Billing.API.Data;
using Billing.API.Services.Repositories.Abstractions;
using Billing.API.Services.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Shared.Services.IdentityConverter.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.API.Services.Services.Implementations
{
    public class InvoiceValidationService : IInvoiceValidationService
    {
        private InvoicesDbContext _dbContext;

        public InvoiceValidationService(InvoicesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> IsUsersOrder(string userID, int orderID) => _dbContext.UserInvoices.Include(ui => ui.Invoices)
            .AnyAsync(ui => ui.UserID == userID
                            && ui.Invoices.Any(i => i.OrderID == orderID));
    }
}
