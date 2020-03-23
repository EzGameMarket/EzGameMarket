using System.ComponentModel.DataAnnotations;

namespace CartService.API.Models.ViewModels
{
    public class CartItemModifyModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}