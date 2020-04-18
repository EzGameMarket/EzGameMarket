using Services.Billing.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Billing.Shared.Services.Abstractions
{
    public interface IBillingRepository
    {
        Task<InvoiceCreationResultViewModel> Bill(BillViewModel model);

        Task<IEnumerable<BillViewModel>> GetAll();
        Task<BillViewModel> GetByID(string id);
        Task<IEnumerable<BillViewModel>> GetByIDs(IEnumerable<string> id);

        Task<string> Storno(string id);

        Task SendOutToEmail(string id);
    }
}
