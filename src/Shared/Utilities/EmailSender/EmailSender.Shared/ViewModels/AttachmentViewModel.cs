using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Shared.Utilities.EmailSender.Shared.ViewModels
{
    public class AttachmentViewModel
    {
        public string FileName { get; set; }

        public Stream FileStream { get; set; }

        public string ContentType { get; set; }
    }
}
