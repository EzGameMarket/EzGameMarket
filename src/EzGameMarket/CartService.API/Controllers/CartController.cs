using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CartService.API.Data;
using CartService.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CartService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private CartDbContext _dbContext;

        public CartController(CartDbContext db)
        {
            _dbContext = db;
        }

        public async Task<ActionResult<Cart>> GetCart()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return Unauthorized();
            }

            var idClaim = User.Claims.FirstOrDefault(c=> c.Type == ClaimTypes.NameIdentifier);

            if (idClaim != default)
            {
                var id = idClaim.Value;
                var cart = await _dbContext.Cart.FirstOrDefaultAsync(c => c.OwnerID == id);

                if (cart == default)
                { 
                    await CreateCart();

                    cart = await _dbContext.Cart.FirstOrDefaultAsync(c => c.OwnerID == id);
                }

                return cart;
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> CreateCart()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return Unauthorized();
            }

            var idClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (idClaim != default)
            {
                var id = idClaim.Value;

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

        public async Task<IActionResult> Checkout()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return Unauthorized();
            }

            var idClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (idClaim != default)
            {
                var id = idClaim.Value;
                var cart = await _dbContext.Cart.FirstOrDefaultAsync(c => c.OwnerID == id);

                cart.Checkout();

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}