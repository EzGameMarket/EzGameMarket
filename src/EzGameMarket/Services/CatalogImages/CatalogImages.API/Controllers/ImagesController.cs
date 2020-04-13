using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogImages.API.Models;
using CatalogImages.API.Services.Repositories.Abstractions;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.API.Services.Service.Abstractions;
using CatalogImages.API.ViewModels.Image;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.CloudStorage.Shared.Services.Abstractions;

namespace CatalogImages.API.Controllers
{
    [Route("api/catalog/image/manager")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private ICatalogItemImageService _catalogItemImageService;
        private ICatalogImageRepository _catalogImageRepository;
        private IStorageService _storageService;

        public ImagesController(
            ICatalogItemImageService catalogItemImageService,
            ICatalogImageRepository catalogImageRepository,
            IStorageService storageService)
        {
            _catalogItemImageService = catalogItemImageService;
            _catalogImageRepository = catalogImageRepository;
            _storageService = storageService;
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

            var tasks = model.Images.Select(async image =>
            {
                await ManageUploadedImage(image);
            });

            return default;
        }

        private async Task ManageUploadedImage(IFormFile image)
        {
            //test.png
            var fileNameWithExtension = image.FileName;


            if (image.Length >= CatalogItemImageModel.MaxFileLength)
            {
                throw new ApplicationException($"A {fileNameWithExtension} fájl mérete meghaladta a {CatalogItemImageModel.MaxFileLength / 1_048_576.0} MB-ot");
            }

            using var stream = image.OpenReadStream();

            await _storageService.UploadFromStreamWithID(fileNameWithExtension, stream);
        }

        [HttpPost]
        [Route("modify/{id}")]
        public async Task<ActionResult> Modify([FromRoute] int id, [FromBody] CatalogItemImageModel model)
        {
            return default;
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            return default;
        }
    }
}