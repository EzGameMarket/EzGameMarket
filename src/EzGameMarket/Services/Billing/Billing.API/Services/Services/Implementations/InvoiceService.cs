using Billing.API.Models;
using Billing.API.Services.Repositories.Abstractions;
using Billing.API.Services.Services.Abstractions;
using Billing.API.ViewModels;
using Shared.Utilities.Billing.Shared.Services.Abstractions;
using Shared.Utilities.Billing.Shared.ViewModels;
using Shared.Utilities.CloudStorage.Shared.Services.Abstractions;
using Shared.Utilities.EmailSender.Shared.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.API.Services.Services.Implementations
{
    public class InvoiceService : IInvoiceService
    {
        private IInvoiceRepository _invoiceRepository;
        private IBillingAPIFileManager _billingAPIFileManager;
        private IBillingService _billingService;
        private IStorageService _storageService;
        private IEmailSenderService _emailSenderService;

        // Számla az online számlázó rendszerben létrehozása (Billingo) - kész
        // Számla fájl létrehozása, feltöltése - kész
        // Számla kiküldése e-mailben
        public async Task Create(InvoiceCreationViewModel model)
        {
            // a model.Invoice.InvoiceIDt fogja frissíteni az adatbázisban
            await UploadToBillingSystem(model.Invoice);
            // a model.Invoice.InvoiceID frissült a DBben, szóval ebben a modelben is foge?
            using var file = await UploadFileToOurStorage(model);
        }

        private async Task<Stream> UploadFileToOurStorage(InvoiceCreationViewModel model)
        {
            var fileStream = await _billingAPIFileManager.DownloadInvoice(model.Invoice.InvoiceID);
            var uploadResult = await _storageService.UploadWithContainerExtension(model.UserID, fileStream);

            if (uploadResult == false)
            {
                throw new ApplicationException($"A {model.Invoice.InvoiceID} fájl feltöltése közben hiba lépett fel");
            }

            return fileStream;
        }

        // Számla stronozása a számlázó rendszerben
        // Stornozott számla készítése és feltöltése
        public Task Storno(Invoice invoice)
        {
            throw new NotImplementedException();
        }

        public async Task UploadToBillingSystem(Invoice invoice)
        {
            var res = await _billingService.CreateInvoiceAsync(new BillViewModel());

            await _invoiceRepository.UploadBillingSystemID(res.BillingSystemID);
        }
    }
}
