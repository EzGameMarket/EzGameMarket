using Shared.Extensions.HttpClientHandler;
using Services.Billing.Billingo.Models;
using Services.Billing.Shared.Services.Abstractions;
using Services.Billing.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Billing.Billingo
{
    public class BillingoBillingRepository : IBillingRepository
    {
        private BillingoSettingsModel _settings;
        private IHttpHandlerUtil _httpHandler;

        public Task<InvoiceCreationResultViewModel> Bill(BillViewModel model)
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

        public Task<string> Storno(string id)
        {
            throw new NotImplementedException();
        }
    }
}
