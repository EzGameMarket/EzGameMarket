using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogService.API.Models;
using CatalogService.API.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Pagination;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class SearchController : ControllerBase
    {
        public Task<PaginationViewModel<CatalogItem>> Search(string query)
        {
            return default;
        }
    }
}