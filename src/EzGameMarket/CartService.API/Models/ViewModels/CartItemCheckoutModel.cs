using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CartService.API.Models.ViewModels
{
    public class CartItemCheckoutModel
    {
        [Required]
        [DataType(DataType.ImageUrl)]
        public string ProductID { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string ProductName { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string ImageUrl { get; set; }
    }
}
