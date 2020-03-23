﻿using CatalogService.API.Data;
using CatalogService.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductImagesController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public ProductImagesController(ProductDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductImage>>> GetProductImages()
        {
            return await _context.ProductImages.ToListAsync();
        }

        // GET: api/ProductImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductImage>> GetProductImage(int id)
        {
            var productImage = await _context.ProductImages.FindAsync(id);

            if (productImage == null)
            {
                return NotFound();
            }

            return productImage;
        }

        // PUT: api/ProductImages/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductImage(int id, ProductImage productImage)
        {
            if (id != productImage.ID)
            {
                return BadRequest();
            }

            _context.Entry(productImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductImageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductImages
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ProductImage>> PostProductImage(ProductImage productImage)
        {
            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductImage", new { id = productImage.ID }, productImage);
        }

        // DELETE: api/ProductImages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductImage>> DeleteProductImage(int id)
        {
            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }

            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync();

            return productImage;
        }

        private bool ProductImageExists(int id)
        {
            return _context.ProductImages.Any(e => e.ID == id);
        }
    }
}