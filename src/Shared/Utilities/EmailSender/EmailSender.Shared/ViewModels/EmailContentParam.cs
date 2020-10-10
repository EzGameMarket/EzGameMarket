using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Utilities.EmailSender.Shared.ViewModels
{
    public class EmailContentParam
    {
        public string Name { get; set; }

        public object Data { get; set; }

        public override string ToString() => Data.ToString();
    }
}
