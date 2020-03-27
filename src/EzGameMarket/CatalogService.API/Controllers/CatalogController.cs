using CatalogService.API.Data;
using CatalogService.API.Models;
using CatalogService.API.Models.Pagination;
using CatalogService.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("/products")]
        public async Task<ActionResult<PaginationViewModel<Product>>> GetItems([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 30)
        {
            var items = await _catalogService.GetItemsAsync(pageIndex*pageSize,pageSize);
            var allCount = await _catalogService.GetAllItemsCount();

            return Ok(new PaginationViewModel<Product>(allCount,pageIndex,pageSize,items));
        }
    }
}