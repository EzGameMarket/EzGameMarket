using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Gtw.Models.ViewModels.Catalog.SingleProduct
{
    public class Tags
    {
        public string ProductID { get; set; }
        public IEnumerable<string> TagsData { get; set; }
    }
}
