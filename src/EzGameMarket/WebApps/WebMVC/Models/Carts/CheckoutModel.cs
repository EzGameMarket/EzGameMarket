using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models.Carts
{
    public class CheckoutModel
    {
        [Required]
        public List<Address> CostumerAddress { get; set; }
        [Required]
        public Cart BuyerCart { get; set; }
        [Required]
        public Costumer Costumer { get; set; }
    }
}
