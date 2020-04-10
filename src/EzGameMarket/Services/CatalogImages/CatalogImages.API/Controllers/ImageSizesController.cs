using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatalogImages.API.Data;
using CatalogImages.API.Models;
using CatalogImages.API.Extensions.ImageSize;

namespace CatalogImages.API.Controllers
{
    [Route("api/catalog/image/sizes")]
    [ApiController]
    public class ImageSizesController : ControllerBase
    {
        private readonly CatalogImagesDbContext _context;

        public ImageSizesController(CatalogImagesDbContext context)
        {
            _context = context;
        }

        // GET: api/ImageSizes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageSizeModel>>> GetImageSizes()
        {
            return await _context.ImageSizes.ToListAsync();
        }

        // GET: api/ImageSizes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageSizeModel>> GetImageSize(int? id)
        {
            if (id == default || id <= 0)
            {
                return BadRequest();
            }

            var imageSize = await _context.ImageSizes.FindAsync(id);

            if (imageSize == null)
            {
                return NotFound();
            }

            return imageSize;
        }

        // PUT: api/ImageSizes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult> PutImageSize(int? id, ImageSizeModel imageSize)
        {
            var validateResult = await this.ValidateModifyData(id, imageSize);

            if (validateResult != default)
            {
                return validateResult;
            }

            _context.Entry(imageSize).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/ImageSizes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult> PostImageSize(ImageSizeModel imageSize)
        {
            var validateResult = await this.ValidateUploadData(imageSize);

            if (validateResult != default)
            {
                return validateResult;
            }

            _context.ImageSizes.Add(imageSize);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/ImageSizes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteImageSize(int? id)
        {
            if (id == default || id <= 0)
            {
                return BadRequest();
            }

            var imageSize = await _context.ImageSizes.FindAsync(id);
            if (imageSize == null)
            {
                return NotFound();
            }

            _context.ImageSizes.Remove(imageSize);
            await _context.SaveChangesAsync();

            return Ok();
        }

        public Task<bool> ImageSizeExists(int? id)
        {
            return _context.ImageSizes.AnyAsync(e => e.ID == id);
        }
    }
}
