using Shared.Utilities.EmailSender.Core.ViewModels.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.EmailSender.Core.Services.Abstractions
{
    public interface IEmailSenderRepository
    {
        Task<bool> SendMail(SendEmailRepositoryViewModel model);
        Task<bool> SendMail(SendEmailRepositoryViewModel model, SendEmailCcSViewModel cssModel);
        Task<bool> SendMailWithAttachements(SendEmailWithAttachmentsRepositoryViewModel model);
        Task<bool> SendMailWithAttachements(SendEmailWithAttachmentsRepositoryViewModel model, SendEmailCcSViewModel cssModel);
    }
}
