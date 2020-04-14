using Shared.Utilities.Billing.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.Billing.Shared.Services.Abstractions
{
    public interface IBillingService
    {
        Task CreateInvoiceAsync(BillViewModel model);
        Task Strono(string id);

        Task<IEnumerable<BillViewModel>> GetAll();
        Task<BillViewModel> GetByID(string id);
        Task<IEnumerable<BillViewModel>> GetByIDs(IEnumerable<string> ids);
    }
}
