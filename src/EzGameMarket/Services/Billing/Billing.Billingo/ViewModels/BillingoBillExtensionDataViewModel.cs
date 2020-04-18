using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Billing.Billingo.ViewModels
{
    public class BillingoBillExtensionDataViewModel
    {
        public int PaymentMethod { get; set; }

        public string TemplateLangCode { get; set; }

        public int BlockUID { get; set; }
        public int ClientUID { get; set; }
        public int Type { get; set; }
        public int RoundTo { get; set; }
        public int BankAccountUID { get; set; }
    }
}
