using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace Shared.Utilities.EmailSender.Core.ViewModels
{
    public class EmailAttachmentViewModel
    {
        public string FileName { get; set; }

        public Stream FileStream { get; set; }

        public string Disposition { get; set; }
        public string ContentID { get; set; }
        public string Type { get; set; }
    }
}
