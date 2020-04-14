using Shared.Utilities.Billing.Shared.Services.Abstractions;
using Shared.Utilities.Billing.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.Billing.Billingo
{
    public class BillingoBillingRepository : IBillingRepository
    {
        public Task Bill(BillViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BillViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<BillViewModel> GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BillViewModel>> GetByIDs(IEnumerable<string> id)
        {
            throw new NotImplementedException();
        }

        public Task SendOutToEmail(string id)
        {
            throw new NotImplementedException();
        }

        public Task Storno(string id)
        {
            throw new NotImplementedException();
        }
    }
}
