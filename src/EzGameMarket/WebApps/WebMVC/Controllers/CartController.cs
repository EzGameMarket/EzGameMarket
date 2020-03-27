using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMVC.Models;
using WebMVC.Models.Carts;
using WebMVC.Services.Repositorys.Abstractions;

namespace WebMVC.Controllers
{
    public class CartController : Controller
    {
        ILogger<CartController> _logger;
        ICartRepository _cartRepository;

        public CartController(ILogger<CartController> logger,
                              ICartRepository cartRepository)
        {
            _logger = logger;
            _cartRepository = cartRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _cartRepository.GetCartAsync();
            return View(model);
        }

        public IActionResult Checkout()
        {
            var model = new CheckoutModel()
            {
                CostumerAddress = new List<Address>()
                {
                    new Address()
                },
                Costumer = new Costumer() { Email = "werdnikkrisz@gmail.com", PhoneNumber = "+36309136248" },
                BuyerCart = new Cart()
                {
                    BuyerID = "kriszw",
                    CartItems = new List<CartItemModel>()
                    {
                        new CartItemModel(){ ImageUrl = "test.png", Price = 500, ProductID = "minecraft", ProductName = "Minecraft Standard Edition", Quantity = 2 },
                        new CartItemModel(){ ImageUrl = "test.png", Price = 50, ProductID = "bttflvdeled", ProductName = "Battlefield V Deluxe Edition", Quantity = 3 },
                        new CartItemModel(){ ImageUrl = "test.png", Price = 1000, ProductID = "fifa2020", ProductName = "Fifa 20", Quantity = 1 }
                    }
                }
            };
            return View(model);
        }

        //a kosár tartalmát lehet vele frissíteni
        //ha a mennyiség kisebb mint 0 akkor elvesz belőle
        //ha a mennyiség nagyobb mint 0 akkor hozzáadd a kosárhoz
        [HttpPost]
        public IActionResult Update(UpdateCartItemModel model)
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