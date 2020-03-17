using CartService.API.Exceptions;

namespace CartService.API.Models
{
    public class CartItem
    {
        private int _quantity;

        public int ProductID { get; set; }
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