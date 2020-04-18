using Billing.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.API.ViewModels
{
    public class InvoiceCreationViewModel
    {
        public Invoice Invoice { get; set; }

        public string UserID { get; set; }
    }
}
