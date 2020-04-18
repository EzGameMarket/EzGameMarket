using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billing.API.Models;
using Billing.API.Services.Repositories.Abstractions;
using Billing.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Pagination;

namespace Billing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private IInvoiceRepository _invoiceRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Invoice>> GetByID([FromRoute] int id)
        {
            return default;
        }

        [HttpGet]
        [Route("users/{userID}")]
        public async Task<ActionResult<List<Invoice>>> GetByUserID([FromRoute] string userID)
        {
            return default;
        }

        [HttpGet]
        [Route("{orderID}/download")]
        public async Task<ActionResult<List<Invoice>>> GetInvoiceFile([FromRoute] string orderID)
        {
            return default;
        }

        [HttpGet]
        [Route("users/{userID}")]
        public async Task<ActionResult<PaginationViewModel<Invoice>>> GetByUserIDPaginated([FromRoute] string userID, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 30)
        {
            return default;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Invoice>> Create([FromBody] InvoiceCreationViewModel model)
        {
            return default;
        }

        [HttpDelete]
        [Route("cancel/{id}")]
        public async Task<ActionResult<Invoice>> Cancel([FromRoute] int id)
        {
            return default;
        }
    }
}