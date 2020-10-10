using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels.Carts
{
    public class UpdateCartItemModel
    {
        [Required]
        public string ProductID { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
