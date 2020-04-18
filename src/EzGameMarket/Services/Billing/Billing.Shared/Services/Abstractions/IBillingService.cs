using Services.Billing.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Billing.Shared.Services.Abstractions
{
    public interface IBillingService
    {
        Task<InvoiceCreationResultViewModel> CreateInvoiceAsync(BillViewModel model);
        Task<string> Storno(string id);

        Task<IEnumerable<BillViewModel>> GetAll();
        Task<BillViewModel> GetByID(string id);
        Task<IEnumerable<BillViewModel>> GetByIDs(IEnumerable<string> ids);
    }
}
