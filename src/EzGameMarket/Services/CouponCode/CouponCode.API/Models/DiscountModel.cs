using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouponCode.API.Models
{
    public class DiscountModel
    {
        public static int MaximumPercentage = 20;

        public int ID { get; set; }

        public string Name { get; set; }

        public int PercentageDiscount { get; set; }
    }
}
