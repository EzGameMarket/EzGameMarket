using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Shared.Utilities.EmailSender.Core.ViewModels.Repository
{
    public class SendEmailWithAttachmentsRepositoryViewModel : SendEmailRepositoryViewModel
    {
        public SendEmailWithAttachmentsRepositoryViewModel(MailAddress from, string subject, string body, IEnumerable<EmailAttachmentViewModel> attachments) : base(from, subject, body)
        {
            Attachments = attachments;
        }

        public SendEmailWithAttachmentsRepositoryViewModel(MailAddress from, MailAddress to, string subject, string body, IEnumerable<EmailAttachmentViewModel> attachments) : base(from, to, subject, body)
        {
            Attachments = attachments;
        }

        public IEnumerable<EmailAttachmentViewModel> Attachments { get; set; }
    }
}
