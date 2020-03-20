using CartService.API.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace CartService.API.Models
{
    public class CartItem
    {
        private int _quantity;

        [Required]
        [Key]
        public int ProductID { get; set; }
        [Required]
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity + value > 5)
                {
                    throw new TooManyItemsException() { ProductID = ProductID };
                }

                _quantity += value;
            }
        }
    }
}