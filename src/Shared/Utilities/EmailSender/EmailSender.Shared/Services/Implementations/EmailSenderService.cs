using Shared.Utilities.EmailSender.Core.Services.Abstractions;
using Shared.Utilities.EmailSender.Core.ViewModels;
using Shared.Utilities.EmailSender.Core.ViewModels.Repository;
using Shared.Utilities.EmailSender.Shared.Services.Abstractions;
using Shared.Utilities.EmailSender.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.EmailSender.Shared.Services.Implementations
{
    public class EmailSenderService : IEmailSenderService
    {
        private IEmailSenderRepository _emailSenderRepository;

        public EmailSenderService(IEmailSenderRepository emailSenderRepository)
        {
            _emailSenderRepository = emailSenderRepository;
        }

        public async Task<EmailSendResult> SendMail(EmailSendViewModel model)
        {
            var output = new EmailSendResult();

            if (model.Params != default && model.Params.Any() == true)
            {
                model.FillInParams();
            }

            if (model.To.Any())
            {
                var to = model.To.Select(t=> t.ToMailAddress());

                if (model.To.Count() == 1)
                {
                    var sendModel = new SendEmailRepositoryViewModel(model.From.ToMailAddress(), to.FirstOrDefault(), model.Subject, model.Body);
                    var res = await _emailSenderRepository.SendMail(sendModel);
                }
                else
                {
                    var sendModel = new SendEmailRepositoryViewModel(model.From.ToMailAddress(), model.Subject, model.Body);
                    var ccModel = new SendEmailCcSViewModel() { Ccs = to };
                    var res = await _emailSenderRepository.SendMail(sendModel, ccModel);
                }
            }

            return output;
        }

        public async Task<EmailSendResult> SendMail(EmailSendModelWithAttachmentsViewModel model)
        {
            var output = new EmailSendResult();
            
            if (model.Params != default && model.Params.Any() == true)
            {
                model.FillInParams();
            }

            var attachments = model.Attachments.Select(a => new EmailAttachmentViewModel() 
            {
                
            });

            if (model.To.Any())
            {
                var to = model.To.Select(t => t.ToMailAddress());

                if (model.To.Count() == 1)
                {
                    var sendModel = new SendEmailWithAttachmentsRepositoryViewModel(model.From.ToMailAddress(), to.FirstOrDefault(), model.Subject, model.Body, attachments);
                    var res = await _emailSenderRepository.SendMail(sendModel);
                }
                else
                {
                    var sendModel = new SendEmailWithAttachmentsRepositoryViewModel(model.From.ToMailAddress(), model.Subject, model.Body, attachments);
                    var ccModel = new SendEmailCcSViewModel() { Ccs = to };
                    var res = await _emailSenderRepository.SendMail(sendModel, ccModel);
                }
            }

            return output;
        }
    }
}
