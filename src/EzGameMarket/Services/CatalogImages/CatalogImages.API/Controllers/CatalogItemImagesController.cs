using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogImages.API.Models;
using CatalogImages.API.Services.Repositories.Abstractions;
using CatalogImages.API.ViewModels.Image;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogImages.API.Controllers
{
    [Route("api/catalog/")]
    [ApiController]
    public class CatalogItemImagesController : ControllerBase
    {
        private ICatalogItemImageRepository _catalogItemImageRepository;

        public CatalogItemImagesController(ICatalogItemImageRepository catalogItemImageRepository)
        {
            _catalogItemImageRepository = catalogItemImageRepository;
        }

        [HttpGet]
        [Route("{productID}/images")]
        public async Task<ActionResult<List<CatalogItemImageModel>>> GetImagesForProductID([FromRoute] string productID)
        {
            if (string.IsNullOrEmpty(productID))
            {
                return BadRequest();
            }

            var imgs = await _catalogItemImageRepository.GetAllImageForProductID(productID);

            if (imgs != default && imgs.Any())
            {
                return imgs;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{productID}/images/filter")]
        public async Task<ActionResult<List<CatalogItemImageModel>>> GetImagesForProductID([FromRoute] string productID, [FromQuery] string typeName = default, [FromQuery] string sizeName = default)
        {
            if (string.IsNullOrEmpty(productID))
            {
                return BadRequest();
            }

            var imgs = await _catalogItemImageRepository.GetAllImageForProductIDByFiltering(productID,typeName, sizeName);

            if (imgs != default && imgs.Any())
            {
                return imgs;
            }
            else
            {
                return NotFound();
            }
        }
    }
}