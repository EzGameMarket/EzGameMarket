using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogImages.API.Models;
using CatalogImages.API.Services.Repositories.Abstractions;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.API.Services.Service.Abstractions;
using CatalogImages.API.ViewModels.Image;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.CloudStorage.Shared.Extensions;
using Shared.Utilities.CloudStorage.Shared.Services.Abstractions;

namespace CatalogImages.API.Controllers
{
    [Route("api/catalog/image/manager")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private ICatalogItemImageService _catalogItemImageService;
        private ICatalogImageRepository _catalogImageRepository;

        public ImagesController(
            ICatalogItemImageService catalogItemImageService,
            ICatalogImageRepository catalogImageRepository,
            IStorageService storageService)
        {
            _catalogItemImageService = catalogItemImageService;
            _catalogImageRepository = catalogImageRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CatalogItemImageModel>> GetByID(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var img = await _catalogImageRepository.GetByID(id);

            if (img != default)
            {
                return img;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("upload")]
        //Ellenőrzés, hogy a fájlok amiket fel akar tölteni az valós
        public async Task<ActionResult> Upload([FromBody] UploadNewImageViewModel model)
        {
            if (model.Images.Count() > 10)
            {
                return BadRequest("Egyszerre max 10 fájlt tölthetsz fel");
            }

            var tasks = model.Images.Select(image => ManageUploadedImage(image));

            await Task.WhenAll(tasks);

            return Ok();
        }

        private Task ManageUploadedImage(IFormFile image)
        {
            //test.png
            var extension = System.IO.Path.GetExtension(image.FileName);

            var fileNameWithExtension = "".GenerateUniqueID() + extension;

            //File név létrehozás

            if (image.Length >= CatalogItemImageModel.MaxFileLength)
            {
                throw new ApplicationException($"A {image.FileName} fájl mérete meghaladta a {Math.Round(CatalogItemImageModel.MaxFileLength / 1_048_576.0,2)} MB-ot");
            }

            return _catalogItemImageService.UploadImage(fileNameWithExtension, image);
        }

        [HttpPost]
        [Route("modify/{id}")]
        public async Task<ActionResult> Modify([FromRoute] int id, [FromBody] ModifyImageViewModel model)
        {
            if (id <= 0 || string.IsNullOrEmpty(model.ProductID))
            {
                return BadRequest();
            }

            await _catalogItemImageService.ModifyImage(id, model);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            if (id<= 0)
            {
                return BadRequest();
            }

            var cImage = await _catalogImageRepository.GetByID(id);

            await _catalogItemImageService.DeleteImage(cImage.ImageUri);

            await _catalogImageRepository.Delete(id);

            return Ok();
        }
    }
}