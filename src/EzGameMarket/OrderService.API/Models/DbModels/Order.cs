using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderService.API.Models.DbModels
{
    public class Order
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string CustomerID { get; set; }

        [Required]
        public int AddressID { get; set; }

        [Required]
        public List<OrderedItem> Items { get; set; }

        [Required]
        public DateTime OrderedDate { get; set; }

        [Required]
        public OrderState State { get; set; }
    }
}