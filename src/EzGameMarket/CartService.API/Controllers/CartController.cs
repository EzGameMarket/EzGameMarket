﻿using CartService.API.Data;
using CartService.API.Models;
using CartService.API.Models.ViewModels;
using CartService.API.Services;
using EventBus.Shared.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CartService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private CartDbContext _dbContext;
        private IIdentityService _identityService;
        private IEventBusRepository _eventBus;
        private ICartRepository _cartRepository;

        public CartController(CartDbContext db,
                              IIdentityService identityService,
                              IEventBusRepository eventBus,
                              ICartRepository cartRepo)
        {
            _dbContext = db;
            _identityService = identityService;
            _eventBus = eventBus;
            _cartRepository = cartRepo;
        }

        [Route("/")]
        public async Task<ActionResult<Cart>> GetCart()
        {
            var id = _identityService.GetUserID(User);

            if (id != default)
            {
                var cart = await _cartRepository.GetCartByCustomerIDAsync(id);

                return cart;
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("/update")]
        public async Task<IActionResult> Update(CartItemModifyModel model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var id = _identityService.GetUserID(User);

            if (id != default)
            {
                await _cartRepository.UpdateCartAsync(id, model);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("/checkout")]
        public async Task<IActionResult> Checkout(CheckoutModel model)
        {
            if (model == default || ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var id = _identityService.GetUserID(User);

            if (id != default)
            {
                try
                {
                    await _cartRepository.Checkout(id, model);
                }
                catch (System.Exception ex)
                {
                    return BadRequest(ex);
                }

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}