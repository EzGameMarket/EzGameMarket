using Shared.Utilities.EmailSender.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.EmailSender.Shared.Services.Abstractions
{
    public interface IEmailSenderService
    {
        Task<EmailSendResult> SendMail(EmailSendViewModel model);
        Task<EmailSendResult> SendMail(EmailSendModelWithAttachmentsViewModel model);
    }
}
