using System.Linq;
using CatalogService.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Shared.Extensions.Pagination;
using CatalogService.API.ViewModels.Products;
using CatalogService.API.Services.Service.Abstractions;
using System.Collections.Generic;

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
        [Route("products")]
        public async Task<ActionResult<PaginationViewModel<CatalogItem>>> GetItems([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 30)
        {
            var data = await _catalogService.GetItemsAsync(pageIndex * pageSize, pageSize);
            var allCount = await _catalogService.GetAllItemsCount();

            return new PaginationViewModel<CatalogItem>(allCount, pageIndex, pageSize, data);
        }
        [HttpGet]
        [Route("product/{productID}")]
        public async Task<ActionResult<ProductViewModel>> GetProductDetail([FromRoute] string productID)
        {
            if (string.IsNullOrEmpty(productID))
            {
                return BadRequest();
            }

            var data = await _catalogService.GetProductAsync(productID);

            if (data != default)
            {
                return data;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{ids}")]
        [Route("items")]
        public async Task<ActionResult<List<CatalogItem>>> GetItemsFromIDS([FromQuery] IEnumerable<string> ids) //ha üres listát/nullot kapnék vissza akkor: [FromQuery](Name = "ids")
        {
            var data = await _catalogService.GetItemsFromIDsAsync(ids);

            return data;
        }
    }
}