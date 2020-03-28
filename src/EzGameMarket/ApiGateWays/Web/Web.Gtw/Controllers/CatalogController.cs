using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Pagination;
using Web.Gtw.Infrastructare.Extensions.Repositories.Abstractions;
using Web.Gtw.Models;
using Web.Gtw.Models.ViewModels.Catalog;

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
        public async Task<ActionResult<PaginationViewModel<CatalogItem>>> GetItems([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 30)
        {
            var items = await _catalogRepository.GetItems(pageIndex, pageSize);

            return Ok(items);
        }
    }
}