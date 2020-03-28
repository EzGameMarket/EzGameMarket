using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Gtw.Infrastructare.Extensions.Repositories.Abstractions;
using Web.Gtw.Models.ViewModels;
using Web.Gtw.Models.ViewModels.Cart;


namespace Web.Gtw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private ICartRepository _cartRepository;

        public ActionResult Update(CartItemUpdateModel model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            return Ok(_cartRepository.Update(model));
        }

        public ActionResult GetCart(string userID)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            return Ok(_cartRepository.GetCart(userID));
        }

        public ActionResult Checkout(CheckoutViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            return Ok(_cartRepository.Checkout(model));
        }
    }
}