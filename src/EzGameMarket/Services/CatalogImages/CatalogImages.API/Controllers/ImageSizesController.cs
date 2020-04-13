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
using CatalogImages.API.Services.Repositories.Abstractions;

namespace CatalogImages.API.Controllers
{
    [Route("api/catalog/image/sizes")]
    [ApiController]
    public class ImageSizesController : ControllerBase
    {
        private readonly IImageSizeRepository _imageSizeRepository;

        public ImageSizesController(IImageSizeRepository imageSizeRepository)
        {
            _imageSizeRepository = imageSizeRepository;
        }

        // GET: api/ImageSizes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageSizeModel>>> GetImageSizes()
        {
             return await _imageSizeRepository.GetAllSizes();
        }

        // GET: api/ImageSizes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageSizeModel>> GetImageSize(int? id)
        {
            if (id == default || id <= 0)
            {
                return BadRequest();
            }

            var imageSize = await _imageSizeRepository.GetByID(id.GetValueOrDefault());

            if (imageSize != null)
            {
                return imageSize;
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/ImageSizes/5
        [HttpGet("{id}")]
        [Route("{id}/images")]
        public async Task<ActionResult<ImageSizeModel>> GetImageSizeWithImages(int? id)
        {
            if (id == default || id <= 0)
            {
                return BadRequest();
            }

            var imageSize = await _imageSizeRepository.GetByIDWithImages(id.GetValueOrDefault());

            if (imageSize != null)
            {
                return imageSize;
            }
            else
            {
                return NotFound();
            }
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

            await _imageSizeRepository.Modify(id.GetValueOrDefault(), imageSize);

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

            await _imageSizeRepository.Add(imageSize);

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

            if(await ImageSizeExists(id) == false)
            {
                return NotFound();
            }

            await _imageSizeRepository.Delete(id.GetValueOrDefault());

            return Ok();
        }

        public Task<bool> ImageSizeExists(int? id) => _imageSizeRepository.AnyWithID(id.GetValueOrDefault());
    }
}
