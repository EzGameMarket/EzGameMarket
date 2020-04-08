using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Gtw.Models.ViewModels.Catalog.SingleProduct
{
    public class Categories
    {
        public string ProductID { get; set; }
        public IEnumerable<string> CategoriesData { get; set; }
    }
}
