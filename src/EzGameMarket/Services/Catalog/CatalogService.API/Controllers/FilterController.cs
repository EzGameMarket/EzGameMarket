using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogService.API.Services.Service.Abstractions;
using CatalogService.API.ViewModels.Products;
using CatalogService.API.ViewModels.Products.SearchableExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Pagination;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private IFilterService _filterService;

        public FilterController(IFilterService filterService)
        {
            _filterService = filterService;
        }

        [HttpGet]
        [Route("filter")]
        public async Task<ActionResult<PaginationViewModel<CatalogItem>>> GetFilteredItems([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 30, [FromQuery] IEnumerable<string> categories = null, [FromQuery] IEnumerable<string> tags = null)
        {
            if (categories == default && tags == default)
            {
                return BadRequest();
            }

            var skip = pageIndex * pageSize;
            var take = pageSize;

            var items = await GetItems(skip, take, categories, tags);

            return new PaginationViewModel<CatalogItem>(items.AllCount,pageIndex,pageSize,items.Data);
        }
        
        private Task<ContainsAllCountModel<CatalogItem>> GetItems(int skip, int take, IEnumerable<string> categories = null, IEnumerable<string> tags = null)
        {
            if (categories != default && tags == default)
            {
                return _filterService.FilterByCategoriesAsync(skip, take, categories);
            }
            else if (categories == default && tags != default)
            {
                return _filterService.FilterByTagsAsync(skip, take, tags);
            }
            else if(categories != default && tags != default)
            {
                return _filterService.FilterCategoriesAndTagsAsync(skip,take,categories,tags);
            }
            else
            {
                return default;
            }
        }
    }
}