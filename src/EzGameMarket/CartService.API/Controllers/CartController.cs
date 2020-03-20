using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CartService.API.Data;
using CartService.API.Models;
using CartService.API.Models.ViewModels;
using CartService.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CartService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private CartDbContext _dbContext;
        private IIdentityService _identityService;

        public CartController(CartDbContext db,
                              IIdentityService identityService)
        {
            _dbContext = db;
            _identityService = identityService;
        }

        public async Task<ActionResult<Cart>> GetCart()
        {
            var id = _identityService.GetUserID(User);

            if (id != default)
            {
                var cart = await _dbContext.Cart.FirstOrDefaultAsync(c => c.OwnerID == id);

                if (cart == default)
                { 
                    var res = await CreateCart();

                    if (res is OkResult)
                    {
                        cart = await _dbContext.Cart.FirstOrDefaultAsync(c => c.OwnerID == id);
                    }
                    else
                    {
                        return NotFound();
                    }
                }

                return cart;
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> AddItem(CartItemModifyModel model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var id = _identityService.GetUserID(User);

            if (id != default)
            {
                var cart = await _dbContext.Cart.FirstOrDefaultAsync(c => c.OwnerID == id);

                cart.AddItem(model);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> RemoveItem(CartItemModifyModel model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var id = _identityService.GetUserID(User);

            if (id != default)
            {
                var cart = await _dbContext.Cart.FirstOrDefaultAsync(c => c.OwnerID == id);

                cart.RemoveItem(model);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> CreateCart()
        {
            var id = _identityService.GetUserID(User);

            if (id != default)
            {
                var cart = new Cart() { OwnerID = id };

                await _dbContext.Cart.AddAsync(cart);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Checkout(CheckoutModel model)
        {
            if (model == default || ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var id = _identityService.GetUserID(User);

            if (id != default)
            {
                var cart = await _dbContext.Cart.FirstOrDefaultAsync(c => c.OwnerID == id);

                cart.Checkout(model);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}