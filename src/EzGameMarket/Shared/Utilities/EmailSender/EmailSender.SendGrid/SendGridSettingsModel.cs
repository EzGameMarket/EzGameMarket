using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Shared.Utilities.EmailSender.SendGridProvider
{
    public class SendGridSettingsModel
    {
        public string API_KEY { get; set; }

        public MailAddress From { get; set; }
    }
}
