using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogImages.API.Models;
using CatalogImages.API.ViewModels.Image;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogImages.API.Controllers
{
    [Route("api/catalog/image/manager")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

        [HttpPost]
        [Route("upload")]
        //Ellenőrzés, hogy a fájlok amiket fel akar tölteni az valós
        public async Task<ActionResult> Upload([FromBody] UploadNewImageViewModel model)
        {
            return default;
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