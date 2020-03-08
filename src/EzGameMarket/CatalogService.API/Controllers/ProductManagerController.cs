using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogService.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductManagerController : ControllerBase
    {
        private ProductDbContext _context;
        private ILogger<ProductManagerController> _logger;

        public ProductManagerController(ProductDbContext context,
                                        ILogger<ProductManagerController> logger)
        {
            _context = context;
            _logger = logger;
        }

        
    }
}