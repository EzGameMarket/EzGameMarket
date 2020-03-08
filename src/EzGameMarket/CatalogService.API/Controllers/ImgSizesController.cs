using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatalogService.API.Data;
using CatalogService.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImgSizesController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public ImgSizesController(ProductDbContext context)
        {
            _context = context;
        }

        // GET: api/ImgSizes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImgSize>>> GetImgSizes()
        {
            return await _context.ImgSizes.ToListAsync();
        }

        // GET: api/ImgSizes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImgSize>> GetImgSize(int id)
        {
            var imgSize = await _context.ImgSizes.FindAsync(id);

            if (imgSize == null)
            {
                return NotFound();
            }

            return imgSize;
        }

        // PUT: api/ImgSizes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImgSize(int id, ImgSize imgSize)
        {
            if (id != imgSize.ID)
            {
                return BadRequest();
            }

            _context.Entry(imgSize).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImgSizeExists(id))
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

        // POST: api/ImgSizes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ImgSize>> PostImgSize(ImgSize imgSize)
        {
            _context.ImgSizes.Add(imgSize);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImgSize", new { id = imgSize.ID }, imgSize);
        }

        // DELETE: api/ImgSizes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ImgSize>> DeleteImgSize(int id)
        {
            var imgSize = await _context.ImgSizes.FindAsync(id);
            if (imgSize == null)
            {
                return NotFound();
            }

            _context.ImgSizes.Remove(imgSize);
            await _context.SaveChangesAsync();

            return imgSize;
        }

        private bool ImgSizeExists(int id)
        {
            return _context.ImgSizes.Any(e => e.ID == id);
        }
    }
}
