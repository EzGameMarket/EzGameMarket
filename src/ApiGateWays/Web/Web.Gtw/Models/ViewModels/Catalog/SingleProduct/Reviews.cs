using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Gtw.Models.ViewModels.Catalog.SingleProduct
{
    public class Reviews
    {
        public double Rate { get; set; }

        public IEnumerable<Review> ReviewsData { get; set; }
    }
}
