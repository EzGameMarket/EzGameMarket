using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Shared.Utilities.EmailSender.Shared.ViewModels
{
    public class EmailAddress
    {
        public EmailAddress(string email, string displayName)
        {
            Email = email;
            DisplayName = displayName;
        }

        public EmailAddress(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
        public string DisplayName { get; set; }

        public MailAddress ToMailAddress() => new MailAddress(Email, DisplayName);
    }
}
