using Billing.API.Exceptions.Invoices;
using Billing.API.Models;
using Billing.API.Services.Repositories.Abstractions;
using Billing.API.Services.Services.Abstractions;
using Billing.API.ViewModels;
using Newtonsoft.Json.Serialization;
using Services.Billing.Shared.Services.Abstractions;
using Services.Billing.Shared.ViewModels;
using Shared.Utilities.CloudStorage.Shared.Services.Abstractions;
using Shared.Utilities.EmailSender.Shared.Services.Abstractions;
using Shared.Utilities.EmailSender.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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

        public InvoiceService(IInvoiceRepository invoiceRepository,
                              IBillingAPIFileManager billingAPIFileManager,
                              IBillingService billingService,
                              IStorageService storageService,
                              IEmailSenderService emailSenderService)
        {
            _invoiceRepository = invoiceRepository;
            _billingAPIFileManager = billingAPIFileManager;
            _billingService = billingService;
            _storageService = storageService;
            _emailSenderService = emailSenderService;
        }

        // Számla az online számlázó rendszerben létrehozása (Billingo) - kész
        // Számla fájl létrehozása, feltöltése - kész
        // Számla kiküldése e-mailben
        public async Task Create(InvoiceCreationViewModel model)
        {
            // a model.Invoice.InvoiceIDt fogja frissíteni az adatbázisban
            await UploadToBillingSystem(model.Invoice);
            // a model.Invoice.InvoiceID frissült a DBben, szóval ebben a modelben is foge?
            using var file = await UploadFileToOurStorage(model);
            var emailSendModel = CreateEmailSendModel(model, file);
            await _emailSenderService.SendMail(emailSendModel);
        }

        private EmailSendModelWithAttachmentsViewModel CreateEmailSendModel(InvoiceCreationViewModel model, Stream file)
        {
            return new EmailSendModelWithAttachmentsViewModel()
            {
                Subject = $"#{model.Invoice.OrderID} rendelés e-számlája",
                From = new EmailAddress("billing@kwsoft.dev", $"E-Számlázás EzG"),
                To = new List<EmailAddress>() { new EmailAddress(model.Invoice.UserEmail, $"{model.Invoice.LastName} {model.Invoice.FirstName} ") },
                Attachments = new List<AttachmentViewModel>() 
                { 
                    new AttachmentViewModel() 
                    {
                        FileName = $"{model.Invoice.OrderID}-szamla.pdf",
                        FileStream = file,
                        ContentType = "application/pdf"
                    }
                },
                Body = "",
                
            };
        }

        private async Task<Stream> UploadFileToOurStorage(InvoiceCreationViewModel model)
        {
            var fileStream = await _billingAPIFileManager.DownloadInvoice(model.Invoice.BillingSystemInvoiceID);
            var uploadResult = await _storageService.UploadWithContainerExtension(model.UserID, model.Invoice.OrderID.ToString() ,fileStream);

            if (uploadResult.Success == false)
            {
                throw new ApplicationException($"A {model.Invoice.BillingSystemInvoiceID} fájl feltöltése közben hiba lépett fel");
            }

            await _invoiceRepository.UpdateFilePath(uploadResult.FileAbsoluteUrl, model.Invoice.ID.GetValueOrDefault());

            return fileStream;
        }

       
        // Számla stronozása a számlázó rendszerben - kész
        // Stornozott számla készítése és feltöltése - kell-e?
        public async Task Storno(Invoice invoice)
        {
            if (invoice.Canceled == true)
            {
                throw new InvoiceAlreadyCanceledException() { InvoiceID = invoice.ID.GetValueOrDefault(), OrderID = invoice.OrderID };
            }

            var cancelInvoiceID = await CancelInvoice(invoice);

            await _invoiceRepository.UploadCanceledInvoiceBillingSystemID(cancelInvoiceID, invoice.ID.GetValueOrDefault());
        }

        public Task<string> CancelInvoice(Invoice invoice) => _billingService.Storno(invoice.BillingSystemInvoiceID);

        private async Task UploadToBillingSystem(Invoice invoice)
        {
            var res = await _billingService.CreateInvoiceAsync(default);

            await _invoiceRepository.UploadBillingSystemID(res.BillingSystemID, invoice.ID.GetValueOrDefault());
        }
    }
}
