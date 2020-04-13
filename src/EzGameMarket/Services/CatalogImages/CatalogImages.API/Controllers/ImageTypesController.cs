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
using CatalogImages.API.Services.Repositories.Abstractions;

namespace CatalogImages.API.Controllers
{
    [Route("api/catalog/images/type")]
    [ApiController]
    public class ImageTypesController : ControllerBase
    {
        private readonly IImageTypeRepository _imageTypeRepository;

        public ImageTypesController(IImageTypeRepository imageTypeRepository)
        {
            _imageTypeRepository = imageTypeRepository;
        }

        // GET: api/ImageTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageTypeModel>>> GetImageTypes()
        {
            return await _imageTypeRepository.GetAllTypes();
        }

        // GET: api/ImageTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageTypeModel>> GetImageType(int? id)
        {
            if (id == default || id <= 0)
            {
                return BadRequest();
            }

            var imageType = await _imageTypeRepository.GetByID(id.GetValueOrDefault());

            if (imageType == null)
            {
                return NotFound();
            }

            return imageType;
        }

        [HttpGet("{id}")]
        [Route("{id}/images")]
        public async Task<ActionResult<ImageTypeModel>> GetImageTypeWithImages(int? id)
        {
            if (id == default || id <= 0)
            {
                return BadRequest();
            }

            var imageType = await _imageTypeRepository.GetByIDWithImages(id.GetValueOrDefault());

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

            await _imageTypeRepository.Modify(id.GetValueOrDefault(), imageType);

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

            await _imageTypeRepository.Add(imageType);

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

            var imageType = await _imageTypeRepository.GetByID(id.GetValueOrDefault());
            if (imageType == null)
            {
                return NotFound();
            }

            await _imageTypeRepository.Delete(id.GetValueOrDefault());

            return Ok();
        }

        public Task<bool> ImageTypeExists(int? id) => _imageTypeRepository.AnyWithID(id.GetValueOrDefault());

        public Task<bool> ImageTypeWithNameExists(string name) => _imageTypeRepository.AnyWithName(name);
    }
}
