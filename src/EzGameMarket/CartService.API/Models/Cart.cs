using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CartService.API.Models
{
    public class Cart
    {
        [Required]
        public int CartID { get; set; }
        [Required]
        public string OwnerID { get; set; }

        public List<CartItem> Items { get; set; }

        public void Checkout()
        {

        }
    }
}
