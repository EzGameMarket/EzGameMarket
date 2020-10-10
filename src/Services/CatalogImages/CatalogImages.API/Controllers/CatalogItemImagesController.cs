using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogImages.API.Exceptions.Images.Model;
using CatalogImages.API.Exceptions.ImageSize.Model;
using CatalogImages.API.Exceptions.ImageType.Model;
using CatalogImages.API.Models;
using CatalogImages.API.Services.Repositories.Abstractions;
using CatalogImages.API.Services.Service.Abstractions;
using CatalogImages.API.ViewModels.Image;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogImages.API.Controllers
{
    [Route("api/catalog/")]
    [ApiController]
    public class CatalogItemImagesController : ControllerBase
    {
        private ICatalogItemImageService _catalogItemImageService;
        private ICatalogImageRepository _catalogItemImageRepository;

        public CatalogItemImagesController(ICatalogItemImageService catalogItemImageService,
                                           ICatalogImageRepository catalogItemImageRepository = default)
        {
            _catalogItemImageService = catalogItemImageService;
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

            var imgs = await _catalogItemImageService.GetAllImageForProductID(productID);

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
        public async Task<ActionResult<List<CatalogItemImageModel>>> GetImagesForProductIDWithFiltering([FromRoute] string productID, [FromQuery] string typeName = default, [FromQuery] string sizeName = default)
        {
            if (string.IsNullOrEmpty(productID))
            {
                return BadRequest();
            }

            var imgs = await _catalogItemImageService.GetAllImageForProductIDByFiltering(productID,typeName, sizeName);

            if (imgs != default && imgs.Any())
            {
                return imgs;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("{id}/set/size")]
        public async Task<ActionResult> ChangeSize([FromRoute] int id, [FromRoute] int sizeID)
        {
            if (id <= 0)
            {
                return BadRequest($"A megadott kép azonosítója: {id} érvénytelen, nem lehet kisebb egynél");
            }

            if (sizeID <= 0)
            {
                return BadRequest($"A megadott kép méret azonosítója: {sizeID} érvénytelen, nem lehet kisebb egynél");
            }

            try
            {
                await _catalogItemImageRepository.ModifySize(id, sizeID);

                return Ok();
            }
            catch (CImageNotFoundException)
            {
                return NotFound($"Nem létezik feltöltött kép a {id} azonosítóval");
            }
            catch (ImageSizeNotFoundByIDException)
            {
                return NotFound($"Nem létezik kép méret a {sizeID} azonosítóval");
            }
        }

        [HttpPost]
        [Route("{id}/set/type")]
        public async Task<ActionResult> ChangeType([FromRoute] int id, [FromRoute] int typeID)
        {
            if (id <= 0)
            {
                return BadRequest($"A megadott kép azonosítója: {id} érvénytelen, nem lehet kisebb egynél");
            }

            if (typeID <= 0)
            {
                return BadRequest($"A megadott kép típus azonosítója: {typeID} érvénytelen, nem lehet kisebb egynél");
            }

            try
            {
                await _catalogItemImageRepository.ModifyType(id, typeID);

                return Ok();
            }
            catch (CImageNotFoundException)
            {
                return NotFound($"Nem létezik feltöltött kép a {id} azonosítóval");
            }
            catch (ImageTypeNotFoundByIDException)
            {
                return NotFound($"Nem létezik kép típus a {typeID} azonosítóval");
            }
        }
    }
}