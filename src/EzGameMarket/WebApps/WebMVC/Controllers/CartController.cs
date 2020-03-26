using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebMVC.Models;
using WebMVC.Models.Carts;

namespace WebMVC.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var data = new List<CartItemModel>()
            {
                new CartItemModel(){ ImageUrl = "test.png", Price = 500, ProductID = "minecraft", ProductName = "Minecraft Standard Edition", Quantity = 2 },
                new CartItemModel(){ ImageUrl = "test.png", Price = 50, ProductID = "bttflvdeled", ProductName = "Battlefield V Deluxe Edition", Quantity = 3 },
                new CartItemModel(){ ImageUrl = "test.png", Price = 1000, ProductID = "fifa2020", ProductName = "Fifa 20", Quantity = 1 }
            };
            var model = new Cart()
            {
                BuyerID = "1",
                CartItems = data
            };
            return View(model);
        }

        public IActionResult Checkout()
        {
            var model = new CheckoutModel();
            return View(model);
        }

        //a kosár tartalmát lehet vele frissíteni
        //ha a mennyiség kisebb mint 0 akkor elvesz belőle
        //ha a mennyiség nagyobb mint 0 akkor hozzáadd a kosárhoz
        [HttpPost]
        public IActionResult Update(AddCartItemModel model)
        {
            var prod = model.ProductID;
            var quantity = model.Quantity;

            if (string.IsNullOrEmpty(prod))
            {
                return BadRequest($"The product id must be not empty to add it into the cart");
            }

            if (quantity != 0)
            {
                return BadRequest($"The quantity for {prod} must be not 0 to update the cart");
            }

            //APiGateWay meghívása

            return RedirectToAction("Index","Product");
        }
    }
}