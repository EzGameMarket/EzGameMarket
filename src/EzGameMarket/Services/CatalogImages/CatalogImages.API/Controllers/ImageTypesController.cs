using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatalogImages.API.Data;
using CatalogImages.API.Models;
using CatalogImages.API.Extensions.ImageType;

namespace CatalogImages.API.Controllers
{
    [Route("api/catalog/images/type")]
    [ApiController]
    public class ImageTypesController : ControllerBase
    {
        private readonly CatalogImagesDbContext _context;

        public ImageTypesController(CatalogImagesDbContext context)
        {
            _context = context;
        }

        // GET: api/ImageTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageTypeModel>>> GetImageTypes()
        {
            return await _context.ImageTypes.ToListAsync();
        }

        // GET: api/ImageTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageTypeModel>> GetImageType(int? id)
        {
            if (id == default || id <= 0)
            {
                return BadRequest();
            }

            var imageType = await _context.ImageTypes.FindAsync(id);

            if (imageType == null)
            {
                return NotFound();
            }

            return imageType;
        }

        // PUT: api/ImageTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult> PutImageType(int? id, ImageTypeModel imageType)
        {
            var validateResult = await this.ValidateModifyData(id, imageType);

            if (validateResult != default)
            {
                return validateResult;
            }
                
            _context.Entry(imageType).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/ImageTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult> PostImageType(ImageTypeModel imageType)
        {
            var validateResult = await this.ValidateUploadData(imageType);

            if (validateResult != default)
            {
                return validateResult;
            }

            _context.ImageTypes.Add(imageType);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/ImageTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteImageType(int? id)
        {
            if (id == default || id <= 0)
            {
                return BadRequest();
            }

            var imageType = await _context.ImageTypes.FindAsync(id);
            if (imageType == null)
            {
                return NotFound();
            }

            _context.ImageTypes.Remove(imageType);
            await _context.SaveChangesAsync();

            return Ok();
        }

        public Task<bool> ImageTypeExists(int? id)
        {
            return _context.ImageTypes.AnyAsync(e => e.ID == id);
        }

        public Task<bool> ImageTypeWithNameExists(string name)
        {
            return _context.ImageTypes.AnyAsync(e => e.Name == name);
        }
    }
}
