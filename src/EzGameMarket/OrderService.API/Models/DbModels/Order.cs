using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.API.Models.DbModels
{
    public class Order
    {
        public int ID { get; set; }
        public string CustomerID { get; set; }
        public int AddressID { get; set; }
        public List<OrderedItem> Items { get; set; }
        public DateTime OrderedDate { get; set; }
        public OrderState State { get; set; }
    }
}
