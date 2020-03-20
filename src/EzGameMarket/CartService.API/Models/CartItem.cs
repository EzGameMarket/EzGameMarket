using CartService.API.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace CartService.API.Models
{
    public class CartItem
    {
        public const int MaximumQuantity = 5;

        public CartItem() { }

        public CartItem(int id)
        {
            ProductID = id;
            AddQuantity(1);
        }

        private int _quantity;
        [Key]
        public int ID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int Quantity => _quantity;

        public void AddQuantity(int value)
        {
            if (_quantity + value > MaximumQuantity)
            {
                throw new TooManyItemsException() { ProductID = ProductID };
            }

            _quantity += value;
        }
    }
}