using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogService.API.Data;
using CatalogService.API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CatalogService.API.Models;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CatalogController : ControllerBase
    {
        ProductDbContext _context;
        ILogger<CatalogController> _logger;

        [HttpPost("UpdatePrice")]
        public async Task<IActionResult> UpdatePrice([FromBody] PriceUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = await _context.Products.FirstOrDefaultAsync(p => p.ID == model.ProductID);

                if (item != default)
                {
                    if (model.NewPrice != default)
                    {
                        item.Price = model.NewPrice;
                    }

                    if (model.NewDiscountedPrice != default)
                    {
                        item.DiscountedPrice = model.NewDiscountedPrice;
                    }

                    _context.Update(item);

                    try
                    {
                        await _context.SaveChangesAsync();

                        //a price internal service bus event meghívása

                        return Ok();
                    }
                    catch (Exception)
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return NotFound();
                }
            }

            return BadRequest();
        }

        [HttpPost("CreateDiscount")]
        public async Task<IActionResult> CreateDiscount([FromBody] List<DiscountCreateViewModel> models)
        {
            if (ModelState.IsValid)
            {
                foreach (var model in models)
                {
                    if (model.Product.ID != default)
                    {
                        var item = await _context.Products.FirstOrDefaultAsync(p => p.ID == model.Product.ID);

                        if (item != default)
                        {
                            if (model.DiscountPrice != default)
                            {
                                item.DiscountedPrice = model.DiscountPrice;
                            }

                            _context.Update(item);

                            try
                            {
                                await _context.SaveChangesAsync();

                                //a price internal service bus event meghívása

                                return Ok();
                            }
                            catch (Exception)
                            {
                                return BadRequest();
                            }
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }

            return BadRequest();
        }
    }
}