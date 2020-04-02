using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Gtw.Services.Repositories.Abstractions;
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

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpPost]
        [Route("update/{userID}")]
        public async Task<ActionResult> Update([FromRoute]string userID, [FromBody]CartItemUpdateModel model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            await _cartRepository.Update(userID,model);

            return Ok();
        }

        [HttpGet]
        [Route("{userID}")]
        public async Task<ActionResult<CartViewModel>> GetCart([FromRoute]string userID)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            return await _cartRepository.GetCart(userID);
        }


        [HttpPost]
        [Route("checkout/{userID}")]
        public async Task<ActionResult> Checkout([FromRoute] string userID,[FromBody] CheckoutViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            await _cartRepository.Checkout(userID, model);

            return Ok();
        }
    }
}