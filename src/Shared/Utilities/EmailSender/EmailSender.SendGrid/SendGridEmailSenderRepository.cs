using Shared.Utilities.EmailSender.Core.Services.Abstractions;
using Shared.Utilities.EmailSender.Core.ViewModels.Repository;
using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Mail;
using System.IO;
using Shared.Extensions;

namespace Shared.Utilities.EmailSender.SendGridProvider
{
    public class SendGridEmailSenderRepository : IEmailSenderRepository
    {
        private SendGridSettingsModel _settings;

        private SendGridClient CreateClient() => new SendGridClient(_settings.API_KEY);
            
        public async Task<bool> SendMail(SendEmailRepositoryViewModel model)
        {
            var sendGridClient = CreateClient();

            var msg = CreateMSGForOneReciepment(model);

            var response = await sendGridClient.SendEmailAsync(msg);

            return ValidateResponse(response);
        }

        private bool ValidateResponse(Response response)
        {
            return (int)response.StatusCode >= 200 && (int)response.StatusCode < 300;
        }

        private SendGridMessage CreateMSGForOneReciepment(SendEmailRepositoryViewModel model)
        {
            var from = GetFromAddress(model);
            var to = new EmailAddress(model.To.Address,model.To.DisplayName);
            
            return MailHelper.CreateSingleEmail(from, to, model.Subject, "", model.Body);
        }

        private SendGridMessage CreateMSGForReciepments(SendEmailRepositoryViewModel model, SendEmailCcSViewModel ccModel)
        {
            var from = GetFromAddress(model);
            var to = ccModel.Ccs.Select(t => new EmailAddress(t.Address, t.DisplayName)).ToList();
            if (model.To != default)
            {
                to.Add(new EmailAddress(model.To.Address, model.To.DisplayName));
            }

            return MailHelper.CreateSingleEmailToMultipleRecipients(from, to.ToList(), model.Subject, "", model.Body);
        }

        private EmailAddress GetFromAddress(SendEmailRepositoryViewModel model)
        {
            var fromRaw = model.From != default ? 
                model.From : 
                (_settings.From != default ? 
                    _settings.From : 
                    new MailAddress(
                        "custom-email-notset-noreply@kwsoft.dev", 
                        "Custom email not set"));

            return new EmailAddress(fromRaw.Address, fromRaw.DisplayName);
        }

        public async Task<bool> SendMail(SendEmailRepositoryViewModel model, SendEmailCcSViewModel cssModel)
        {
            var sendGridClient = CreateClient();

            var msg = CreateMSGForReciepments(model, cssModel);

            var response = await sendGridClient.SendEmailAsync(msg);

            return ValidateResponse(response);
        }

        public async Task<bool> SendMailWithAttachements(SendEmailWithAttachmentsRepositoryViewModel model)
        {
            var sendGridClient = CreateClient();
            var attchs = await GetAttachments(model);

            var msg = CreateMSGForOneReciepment(model);
            msg.AddAttachments(attchs);

            var response = await sendGridClient.SendEmailAsync(msg);

            return ValidateResponse(response);
        }

        private static async Task<IEnumerable<SendGrid.Helpers.Mail.Attachment>> GetAttachments(SendEmailWithAttachmentsRepositoryViewModel model)
        {
            var attchsTasks = model.Attachments.Select(async a =>
            {
                return new SendGrid.Helpers.Mail.Attachment()
                {
                    Content = Convert.ToBase64String(await a.FileStream.ToByteArray()),
                    Filename = a.FileName,
                    ContentId = a.ContentID,
                    Disposition = a.Disposition,
                    Type = a.Type
                };
            });

            await Task.WhenAll(attchsTasks);

            return attchsTasks.Select(at => at.Result);
        }

        public async Task<bool> SendMailWithAttachements(SendEmailWithAttachmentsRepositoryViewModel model, SendEmailCcSViewModel cssModel)
        {
            var sendGridClient = CreateClient();
            var attchs = await GetAttachments(model);

            var msg = CreateMSGForReciepments(model,cssModel);
            msg.AddAttachments(attchs);

            var response = await sendGridClient.SendEmailAsync(msg);

            return ValidateResponse(response);
        }
    }
}
