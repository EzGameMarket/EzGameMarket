using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.Events
{
    public class DiscountCreatedEventHandler
    {
        public string ItemID { get; set; }
        public double Price { get; set; }
    }
}
