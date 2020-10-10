using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Shared.Utilities.EmailSender.Core.ViewModels.Repository
{
    public class SendEmailCcSViewModel
    {
        public IEnumerable<MailAddress> Ccs { get; set; }
    }
}
