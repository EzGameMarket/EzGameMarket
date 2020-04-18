using Billing.API.Data;
using Billing.API.Exceptions.Invoices;
using Billing.API.Services.Repositories.Implementations;
using Billing.API.Services.Services.Implementations;
using Billing.API.ViewModels;
using Billing.Tests.FakeImplementations;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shared.Utiliies.CloudStorage.Shared.Models.BaseResult;
using Shared.Utilities.Billing.Shared.Services.Abstractions;
using Shared.Utilities.Billing.Shared.ViewModels;
using Shared.Utilities.CloudStorage.Shared.Services.Abstractions;
using Shared.Utilities.EmailSender.Shared.Services.Abstractions;
using Shared.Utilities.EmailSender.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Billing.Tests.API.Services.InvoiceServiceTests
{
    public class StornoMethodTests
    {
        [Fact]
        public async void Create_ShouldBeOkay()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var expectedSystemID = "cancel";
            var expectedUserID = "kriszw";

            var userInvoiceRepo = new UserInvoiceRepository(dbContext);
            var repo = new InvoiceRepository(dbContext, userInvoiceRepo);

            var model = await CreateModel(dbContext, expectedUserID);
            var service = CreateInvoiceService(repo, model, expectedSystemID);

            //Act
            await service.Storno(model.Invoice);
            var actual = await repo.GetInvoceByID(model.Invoice.ID.GetValueOrDefault());

            //Assert
            Assert.NotNull(actual.BillingSystemCanceledInvoiceID);
            Assert.Equal(expectedSystemID,actual.BillingSystemCanceledInvoiceID);
        }

        [Fact]
        public async void Create_ShouldThrowInvoiceNotFoundException()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var expectedSystemID = "cancel";
            var expectedUserID = "kriszw";

            var userInvoiceRepo = new UserInvoiceRepository(dbContext);
            var repo = new InvoiceRepository(dbContext, userInvoiceRepo);

            var model = await CreateModel(dbContext, expectedUserID);
            var service = CreateInvoiceService(repo, model, expectedSystemID);

            model.Invoice.ID = 100;

            //Act
            var createTask = service.Storno(model.Invoice);

            //Assert
            await Assert.ThrowsAsync<InvoiceNotFoundByIDException>(() => createTask);
        }


        [Fact]
        public async void Create_ShouldThrowInvoiceAlreadyCanceledException()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new InvoicesDbContext(dbOptions);

            var expectedSystemID = "cancel";
            var expectedUserID = "kriszw";

            var userInvoiceRepo = new UserInvoiceRepository(dbContext);
            var repo = new InvoiceRepository(dbContext, userInvoiceRepo);

            var model = await CreateModel(dbContext, expectedUserID);
            var service = CreateInvoiceService(repo, model, expectedSystemID);

            model.Invoice.Canceled = true;

            //Act
            var createTask = service.Storno(model.Invoice);

            //Assert
            await Assert.ThrowsAsync<InvoiceAlreadyCanceledException>(() => createTask);
        }


        private static async Task<InvoiceCreationViewModel> CreateModel(InvoicesDbContext dbContext, string userID)
        {
            var invoice = await dbContext.Invoices.AsNoTracking().Include(i => i.File).Include(i => i.Items).FirstOrDefaultAsync();
            invoice.BillingSystemInvoiceID = "test";
            return new InvoiceCreationViewModel() { UserID = userID, IsCanceledInvoice = false, Invoice = invoice };
        }

        private static InvoiceService CreateInvoiceService(InvoiceRepository repo, InvoiceCreationViewModel model, string expectedSystemID)
        {
            var mockedApiFileManager = new Mock<IBillingAPIFileManager>();
            mockedApiFileManager.Setup(bFile => bFile.DownloadInvoice(expectedSystemID)).ReturnsAsync(default(Stream));

            var mockedBillingService = new Mock<IBillingService>();
            mockedBillingService
                .Setup(bService => bService.Storno("test"))
                .ReturnsAsync(expectedSystemID);

            var mockedStorageService = new Mock<IStorageService>();
            mockedStorageService
                .Setup(sService => sService.UploadWithContainerExtension("canceled", model.Invoice.OrderID.ToString(), default(Stream)))
                .ReturnsAsync(new CloudStorageUploadResult(true, $"{model.Invoice.OrderID}-canceled-szamla.pdf"));

            var mockedEmailSender = new Mock<IEmailSenderService>();
            mockedEmailSender
                .Setup(service => service.SendMail(CreateEmailViewModel(model)))
                .ReturnsAsync(new EmailSendResult()).Verifiable();


            return new InvoiceService(repo, mockedApiFileManager.Object, mockedBillingService.Object,
                mockedStorageService.Object, mockedEmailSender.Object);
        }

        private static EmailSendModelWithAttachmentsViewModel CreateEmailViewModel(InvoiceCreationViewModel model)
        {
            return new EmailSendModelWithAttachmentsViewModel()
            {
                Subject = $"#{model.Invoice.OrderID} rendelés törlésének az e-számlája amit sosem küldünk ki",
                From = new EmailAddress("billing@kwsoft.dev", $"E-Számlázás EzG"),
                To = new List<EmailAddress>() { new EmailAddress(model.Invoice.UserEmail, $"{model.Invoice.LastName} {model.Invoice.FirstName} ") },
                Attachments = new List<AttachmentViewModel>()
                    {
                        new AttachmentViewModel()
                        {
                            FileName = $"{model.Invoice.OrderID}-szamla.pdf",
                            FileStream = new MemoryStream(),
                            ContentType = "application/pdf"
                        }
                    },
                Body = "",
            };
        }
    }
}
