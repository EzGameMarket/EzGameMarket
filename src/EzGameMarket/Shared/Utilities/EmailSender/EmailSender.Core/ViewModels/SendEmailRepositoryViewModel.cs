using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Shared.Utilities.EmailSender.Core.ViewModels.Repository
{
    public class SendEmailRepositoryViewModel
    {
        public SendEmailRepositoryViewModel(MailAddress from, string subject, string body)
        {
            From = from;
            Subject = subject;
            Body = body;
        }

        public SendEmailRepositoryViewModel(MailAddress from, MailAddress to, string subject, string body)
        {
            From = from;
            To = to;
            Subject = subject;
            Body = body;
        }

        public MailAddress From { get; set; }

        public MailAddress To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
