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

        public async Task<ActionResult> Update(CartItemUpdateModel model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            await _cartRepository.Update(model);

            return Ok();
        }

        public async Task<ActionResult<CartViewModel>> GetCart(string userID)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            return await _cartRepository.GetCart(userID);
        }

        public async Task<ActionResult> Checkout(CheckoutViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            await _cartRepository.Checkout(model);

            return Ok();
        }
    }
}