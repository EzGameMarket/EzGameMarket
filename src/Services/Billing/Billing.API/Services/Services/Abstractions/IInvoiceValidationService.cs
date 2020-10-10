using Billing.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.API.Services.Services.Abstractions
{
    public interface IInvoiceValidationService
    {
        Task<bool> IsUsersOrder(string userID, int orderID);
    }
}
