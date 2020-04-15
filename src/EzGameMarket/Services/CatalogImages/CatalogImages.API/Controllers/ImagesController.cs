using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CatalogImages.API.Models;
using CatalogImages.API.Services.Repositories.Abstractions;
using CatalogImages.API.Services.Repositories.Implementations;
using CatalogImages.API.Services.Service.Abstractions;
using CatalogImages.API.ViewModels.Image;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Azure;
using Microsoft.OpenApi.Writers;
using Shared.Extensions.ImageExtensions.ImageValidator.Shared;
using Shared.Extensions.ImageScaler.Abstractions;
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
        private IImageValidatorService _imageValidator;
        private IImageResizerService _imageResizerService;

        public ImagesController(
            ICatalogItemImageService catalogItemImageService,
            ICatalogImageRepository catalogImageRepository,
            IImageValidatorService imageValidator)
        {
            _catalogItemImageService = catalogItemImageService;
            _catalogImageRepository = catalogImageRepository;
            _imageValidator = imageValidator;
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

            var tasks = await UploadToCloudAllUploadedImages(model);

            await UploadAllCatalogItemImageModelsToDB(tasks, model);

            return Ok();
        }

        private async Task UploadAllCatalogItemImageModelsToDB(IEnumerable<Task<string>> tasks, UploadNewImageViewModel model)
        {
            var uploadTasks = tasks.Select(t => _catalogItemImageService
                .AddNewImage(new AddNewImageViewModel(model.ProductID, model.Type,model.Size,t.Result)));

            await Task.WhenAll(tasks);
        }

        private async Task<IEnumerable<Task<string>>> UploadToCloudAllUploadedImages(UploadNewImageViewModel model)
        {
            var tasks = model.Images.Select(image => ManageUploadedImage(image, model));

            await Task.WhenAll(tasks);
            return tasks;
        }

        private async Task<string> ManageUploadedImage(IFormFile image, UploadNewImageViewModel model)
        {
            //test.png
            var extension = System.IO.Path.GetExtension(image.FileName);

            var fileNameWithExtension = "".GenerateUniqueID() + extension;

            //File név létrehozás
            await Task.WhenAll(_imageValidator.ValidateSize(image, CatalogItemImageModel.MaxFileLength),
                               _imageValidator.ValidateExtensions(image),
                               _imageValidator.ValidateDimensions(image, 
                                            new SixLabors.Primitives.Size(ImageSizeModel.MinWidth, ImageSizeModel.MinWidth),
                                            new SixLabors.Primitives.Size(ImageSizeModel.MaxWidth,ImageSizeModel.MaxHeight)));

            using var stream = image.OpenReadStream();
            await _imageResizerService.Resize(stream,model.Size.Width,model.Size.Height);

            await _catalogItemImageService.UploadImage(fileNameWithExtension, image);

            return fileNameWithExtension;
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