using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Utilities.EmailSender.Shared.ViewModels
{
    public class EmailSendViewModel
    {
        public EmailAddress From { get; set; }

        public IEnumerable<EmailAddress> To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public IEnumerable<EmailContentParam> Params { get; set; }

        public void FillInParams()
        {
            foreach (var item in Params)
            {
                Body.Replace($"{{{item.Name}}}", item.ToString());
            }
            ReplaceNotFillingInWithParamsMarks();
        }

        private void ReplaceNotFillingInWithParamsMarks()
        {
            Body.Replace("{!", "");
            Subject.Replace("{!", "");
            Body.Replace("!}", "");
            Subject.Replace("!}", "");
        }
    }
}
