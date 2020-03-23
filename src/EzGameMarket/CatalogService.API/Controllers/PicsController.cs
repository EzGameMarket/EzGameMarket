using CatalogService.API.Data;
using CatalogService.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicsController : ControllerBase
    {
        private ProductDbContext _context;
        private ILogger<PicsController> _logger;

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