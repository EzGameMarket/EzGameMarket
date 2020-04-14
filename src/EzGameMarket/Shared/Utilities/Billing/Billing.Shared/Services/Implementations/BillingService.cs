using EventBus.Shared.Abstraction;
using Shared.Utilities.Billing.Shared.Services.Abstractions;
using Shared.Utilities.Billing.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.Billing.Shared.Services.Implementations
{
    public class BillingService : IBillingService
    {
        private IBillingRepository _billingRepository;
        private IEventBusRepository _eventBus;

        public Task CreateInvoiceAsync(BillViewModel model)
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

        public Task Strono(string id)
        {
            throw new NotImplementedException();
        }
    }
}
