using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMVC.ViewModels;
using WebMVC.ViewModels.Carts;
using WebMVC.Services;
using WebMVC.Services.Repositorys.Abstractions;
using WebMVC.Services.Services.Abstractions;

namespace WebMVC.Controllers
{
    [Route("/cart")]
    [Authorize]
    public class CartController : Controller
    {
        ILogger<CartController> _logger;
        ICartRepository _cartRepository;
        IIdentityService _identityService;

        public CartController(ILogger<CartController> logger,
                              ICartRepository cartRepository,
                              IIdentityService identityService)
        {
            _logger = logger;
            _cartRepository = cartRepository;
            _identityService = identityService;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var userID = _identityService.GetUserID(User);

            if (userID == default)
            {
                return RedirectToAction("Index","Account");
            }

            var model = await _cartRepository.GetCartAsync(userID);
            return View(model);
        }

        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index([FromRoute] string id)
        {
            var model = await _cartRepository.GetCartAsync(id);
            return View(model);
        }

        [HttpGet]
        [Route("checkout")]
        public async Task<IActionResult> Checkout()
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
                    OwnerID = "kriszw",
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

        [HttpPost]
        [Route("checkout")]
        public async Task<IActionResult> StartCheckout([FromBody] CheckoutModel model)
        {
            try
            {
                var userID = _identityService.GetUserID(User);
                await _cartRepository.Checkout(userID,model);

                return Ok();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        //a kosár tartalmát lehet vele frissíteni
        //ha a mennyiség kisebb mint 0 akkor elvesz belőle
        //ha a mennyiség nagyobb mint 0 akkor hozzáadd a kosárhoz
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCartItemModel model)
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
            var userID = _identityService.GetUserID(User);
            await _cartRepository.Update(userID,model);

            return RedirectToAction("Index","Product");
        }
    }
}