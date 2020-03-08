using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogService.API.Data;
using CatalogService.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicsController : ControllerBase
    {
        ProductDbContext _context;
        ILogger<PicsController> _logger;

        public PicsController(ProductDbContext context, ILogger<PicsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("forItemID")]
        public async Task<IActionResult> GetImagesFromItemID([FromQuery] PicQueryViewModel model)
        {
            if (string.IsNullOrEmpty(model.ItemID))
            {
                return BadRequest();
            }

            return NotFound();
        }
    }
}