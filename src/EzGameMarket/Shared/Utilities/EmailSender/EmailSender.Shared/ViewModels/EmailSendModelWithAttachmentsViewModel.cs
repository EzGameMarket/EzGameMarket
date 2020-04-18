using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Utilities.EmailSender.Shared.ViewModels
{
    public class EmailSendModelWithAttachmentsViewModel : EmailSendViewModel
    {
        public IEnumerable<AttachmentViewModel> Attachments { get; set; }
    }
}
