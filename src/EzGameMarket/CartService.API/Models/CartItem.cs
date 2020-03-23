using CartService.API.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace CartService.API.Models
{
    public class CartItem
    {
        public const int MaximumQuantity = 5;

        public CartItem()
        {
        }

        public CartItem(int id)
        {
            ProductID = id;
        }

        private int _quantity;

        [Key]
        public int ID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value > MaximumQuantity)
                {
                    throw new TooManyItemsException() { ProductID = ProductID };
                }

                _quantity = value;
            }
        }
    }
}