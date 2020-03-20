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

        public void Checkout(CheckoutModel model)
        {

        }

        public void AddItem(int itemID)
        {
            if (itemID > -1)
            {
                var currStock = Items.FirstOrDefault(i=> i.ProductID == itemID);

                if (currStock != default)
                {
                    currStock.AddQuantity(1);
                }
                else
                {
                    var newStock = new CartItem(itemID);
                    Items.Add(newStock);
                }
            }
        }

        public void RemoveItem(int itemID)
        {
            if (itemID != default)
            {
                var currStock = Items.FirstOrDefault(i => i.ProductID == itemID);

                if (currStock != default)
                {
                    if (currStock.Quantity > 1)
                    {
                        currStock.AddQuantity(-1);
                    }
                    else
                    {
                        Items.Remove(currStock);
                    }
                }
            }
        }
    }
}
