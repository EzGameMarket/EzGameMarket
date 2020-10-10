using CartService.API.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CartService.API.Models
{
    public class Cart
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        [Required]
        public string OwnerID { get; set; }

        public List<CartItem> Items { get; set; }

        public void Update(CartItemModifyModel model)
        {
            var currStock = Items.FirstOrDefault(i => i.ProductID == model.ProductId);

            if (currStock != default)
            {

                currStock.Quantity = model.Quantity;
            }
            else
            {
                var newStock = new CartItem
                {
                    Quantity = model.Quantity
                };
                Items.Add(newStock);
            }
        }
    }
}