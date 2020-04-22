using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billing.API.Exceptions.Invoices;
using Billing.API.Models;
using Billing.API.Services.Repositories.Abstractions;
using Billing.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Pagination;
using Shared.Services.API.Communication.Models;

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
        public async Task<ActionResult<APIResponse<Invoice>>> GetByID([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var invoice = await _invoiceRepository.GetInvoceByID(id);

            if(invoice != default)
            {
                return new APIResponse<Invoice>(invoice);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<EmptyAPIResponse>> Create([FromBody] InvoiceCreationViewModel model)
        {
            if (model == default || ModelState.IsValid == false || model.Invoice == default)
            {
                return BadRequest();
            }

            if (model.Invoice.OrderID <= 0)
            {
                return BadRequest(new EmptyAPIResponse($"A rendelés azonosító nem lehet kisebb mint 0"));
            }

            if (string.IsNullOrEmpty(model.UserID) == true)
            {
                return BadRequest(new EmptyAPIResponse($"A Felhasználó azonosító nem lehet üres"));
            }

            try
            {
                await _invoiceRepository.Add(model);

                return new EmptyAPIResponse();
            }
            catch(InvoiceAlreadyExistsForOrderID ex)
            {
                return Conflict(new EmptyAPIResponse($"A {ex.OrderID} rendelés azonosítóhoz már létezik egy számla"));
            }
            catch (InvoiceAlreadyExistsWithID ex)
            {
                return Conflict(new EmptyAPIResponse($"A {ex.ID} azonosítóvál már létezik egy számla"));
            }
        }

        [HttpDelete]
        [Route("cancel/{id}")]
        public async Task<ActionResult<EmptyAPIResponse>> Cancel([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _invoiceRepository.Storno(id);

                return new EmptyAPIResponse();
            }
            catch (InvoiceNotFoundByIDException)
            {
                return NotFound();
            }
        }
    }
}