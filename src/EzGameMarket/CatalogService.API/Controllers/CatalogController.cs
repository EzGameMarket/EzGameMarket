using System.Linq;
using CatalogService.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Shared.Extensions.Pagination;
using CatalogService.API.ViewModels.Product;

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
        public async Task<ActionResult<PaginationViewModel<CatalogItem>>> GetItems([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 30)
        {
            var items = await _catalogService.GetItemsAsync(pageIndex*pageSize,pageSize);
            var allCount = await _catalogService.GetAllItemsCount();

            var data = items.Select(p=> new CatalogItem(p.Images.FirstOrDefault(i => i.Size.Size == "Catalog")?.Url ?? "CatalogDefault.jpg",
                                                        p.GameID,
                                                        p.Name,
                                                        p.Price,
                                                        p.DiscountedPrice,
                                                        p.Genres.FirstOrDefault()?.Name ?? "Ismeretlen"));

            return Ok(new PaginationViewModel<CatalogItem>(allCount,pageIndex,pageSize, data));
        }
    }
}