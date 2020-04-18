using Billing.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.API.Services.Repositories.Abstractions
{
    public interface IUserInvoiceRepository
    {
        Task<UserInvoice> GetUserInvoice(string userID);
        Task<bool> AnyUserInvoiceWithUserID(string userID);
        Task<bool> AnyUserInvoiceWithID(int id);

        Task Add(UserInvoice model);

        Task AddInvoiceForUserID(string userID, Invoice invoice);
    }
}
