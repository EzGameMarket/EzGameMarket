using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Utilities.Billing.Shared.ViewModels
{
    public class BillViewModel
    {
        public DateTime FullFillDate { get; set; }
        public DateTime DueDate { get; set; }

        public string Currency { get; set; }
        public string Comment { get; set; }

        public IEnumerable<BillCatalogItemVIewModel> Items { get; set; }
    }
}
