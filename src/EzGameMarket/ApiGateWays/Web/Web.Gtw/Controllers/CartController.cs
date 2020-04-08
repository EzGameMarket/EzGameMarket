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
using Web.Gtw.Services.Services.Abstractions;

namespace Web.Gtw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private ICartRepository _cartRepository;
        private IIdentityService _identityService;

        public CartController(ICartRepository cartRepository, IIdentityService identityService)
        {
            _cartRepository = cartRepository;
            _identityService = identityService;
        }

        [HttpPost]
        [Route("update/")]
        public async Task<ActionResult> Update([FromBody]CartItemUpdateModel model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var userID = _identityService.GetUserID(User);

            await _cartRepository.Update(userID,model);

            return Ok();
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<CartViewModel>> GetCart()
        {
            return await _cartRepository.GetOwnCart();
        }

        [HttpGet]
        [Route("{userID}")]
        public async Task<ActionResult<CartViewModel>> GetCart(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return BadRequest();
            }

            return await _cartRepository.GetCart(userID);
        }

        [HttpPost]
        [Route("checkout/")]
        public async Task<ActionResult> Checkout([FromBody] CheckoutViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var userID = _identityService.GetUserID(User);

            await _cartRepository.Checkout(userID, model);

            return Ok();
        }
    }
}