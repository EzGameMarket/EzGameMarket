using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.Events
{
    public class PriceUpdatedEventMessage
    {
        public string ProductID { get; set; }
        public double NewPrice { get; set; }
    }
}
