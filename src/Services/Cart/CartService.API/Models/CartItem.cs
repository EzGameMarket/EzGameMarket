using CartService.API.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CartService.API.Models
{
    public class CartItem
    {
        public const int MaximumQuantity = 5;

        public CartItem()
        {
        }

        private int _quantity;

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public string ProductName { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string ImageUrl { get; set; }

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