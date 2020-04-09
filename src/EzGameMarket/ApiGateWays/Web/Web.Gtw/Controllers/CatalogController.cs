using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Pagination;
using Web.Gtw.Services.Repositories.Abstractions;
using Web.Gtw.Models;
using Web.Gtw.Models.ViewModels.Catalog;
using Web.Gtw.Models.ViewModels.Catalog.SingleProduct;

namespace Web.Gtw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CatalogController : ControllerBase
    {
        private ICatalogRepository _catalogRepository;

        public CatalogController(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        [HttpGet]
        [Route("items")]
        public async Task<ActionResult<PaginationViewModel<CatalogItem>>> GetItems([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 30)
        {
            var items = await _catalogRepository.GetItems(pageIndex, pageSize);

            return items;
        }

        [HttpGet]
        [Route("product/{productID}/")]
        public async Task<ActionResult<Product>> GetProductDetail([FromRoute] string productID)
        {
            var items = await _catalogRepository.GetDetail(productID);

            return items;
        }
        [HttpGet]
        [Route("recommended/{userID}/")]
        public async Task<ActionResult<IEnumerable<CatalogItem>>> GetRecommended([FromRoute] string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return BadRequest();
            }

            var items = await _catalogRepository.GetRecomended(userID);

            return Ok(items);
        }
    }
}