using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Billing.Shared.ViewModels
{
    public class BillCatalogItemVIewModel
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int NetPrice { get; set; }
        public int VATType { get; set; }
        public string Unit { get; set; }
    }
}
