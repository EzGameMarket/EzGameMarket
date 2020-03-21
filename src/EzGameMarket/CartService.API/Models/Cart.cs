using CartService.API.Models.ViewModels;
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
        [Key]
        public int CartID { get; set; }
        [Required]
        public string OwnerID { get; set; }

        public List<CartItem> Items { get; set; }

        public void AddItem(CartItemModifyModel model)
        {
            var currStock = Items.FirstOrDefault(i=> i.ProductID == model.ProductId);

            if (currStock != default)
            {
                currStock.Quantity = model.Quantity;
            }
            else
            {
                var newStock = new CartItem();
                Items.Add(newStock);
            }
        }

        public void RemoveItem(CartItemModifyModel model)
        {
            var currStock = Items.FirstOrDefault(i => i.ProductID == model.ProductId);

            if (currStock != default)
            {
                if (currStock.Quantity > 1)
                {
                    currStock.Quantity = model.Quantity;
                }
                else
                {
                    Items.Remove(currStock);
                }
            }
        }
    }
}
