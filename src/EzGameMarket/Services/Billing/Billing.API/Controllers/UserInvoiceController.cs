using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Billing.API.Exceptions.UserInvoice;
using Billing.API.Models;
using Billing.API.Services.Repositories.Abstractions;
using Billing.API.Services.Services.Abstractions;
using Billing.API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions;
using Shared.Extensions.Pagination;
using Shared.Services.API.Communication.Models;
using Shared.Services.IdentityConverter.Abstractions;
using Shared.Utilities.CloudStorage.Shared.Services.Abstractions;

namespace Billing.API.Controllers
{
    [Route("api/")]
    [Authorize]
    [ApiController]
    public class UserInvoiceController : ControllerBase
    {
        private readonly IIdentityConverterService _identityService;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUserInvoicesService _userInvoicesService;
        private readonly IStorageService _storageService;

        public UserInvoiceController(IInvoiceRepository invoiceRepository,
                                     IUserInvoicesService userInvoicesService,
                                     IStorageService storageService)
        {
            _invoiceRepository = invoiceRepository;
            _userInvoicesService = userInvoicesService;
            _storageService = storageService;
        }

        [Route("user/{userID}/invoices/all")]
        [HttpGet]
        public async Task<ActionResult<APIResponse<IEnumerable<PaginatedInvoiceViewModel>>>> GetAllInvoice([FromRoute] string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return BadRequest(new APIResponse<IEnumerable<PaginatedInvoiceViewModel>>(default, "A UserID nem lehet üres", false));
            }

            var data = await _userInvoicesService.GetAllPaginatedInvoice(userID);

            return new APIResponse<IEnumerable<PaginatedInvoiceViewModel>>(data);
        }

        [Route("user/{userID}/invoices")]
        [HttpGet]
        public async Task<ActionResult<APIResponse<PaginationViewModel<PaginatedInvoiceViewModel>>>> GetInvoices([FromRoute] string userID, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 30)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return BadRequest(new APIResponse<PaginationViewModel<PaginatedInvoiceViewModel>>(default, "A UserID nem lehet üres", false));
            }

            var skip = pageIndex * pageSize;
            var take = pageSize;

            var data = await _userInvoicesService.GetPaginatedInvoicesForUser(userID, skip, take);

            if (data.TotalItemsCount > 0 && data.Data.Any() == true)
            {
                data.ActualPage = pageIndex;
                data.ItemsPerPage = pageSize;
                return new APIResponse<PaginationViewModel<PaginatedInvoiceViewModel>>(data);
            }
            else
            {
                var msg = $"A {userID} userhez nincs több mint {skip} darab számla";

                if (skip == 0)
                {
                    msg = $"A {userID} felhasználó nem létezik";
                }

                return NotFound(new APIResponse<PaginationViewModel<PaginatedInvoiceViewModel>>(default, msg, false));
            }
        }

        [Route("user/{userID}/invoice/{orderID}")]
        [HttpGet]
        public async Task<ActionResult<APIResponse<Invoice>>> GetInvoice([FromRoute] string userID, [FromRoute] int orderID)
        {
            if (string.IsNullOrEmpty(userID) == true)
            {
                return BadRequest(new APIResponse<Invoice>(default, "A UserID nem lehet üres", false));
            }

            if (orderID <= 0)
            {
                return BadRequest(new APIResponse<Invoice>(default, "Az OrderID nem lehet kisebb mint 0", false));
            }

            var invoice = await _invoiceRepository.GetInvoiceForOrderID(orderID);

            if (invoice != default)
            {
                return new APIResponse<Invoice>(invoice);
            }
            else
            {
                return NotFound(new APIResponse<Invoice>(invoice, 
                    $"A {userID} felhasználónak nem létezik számlája a {orderID} rendelés azonosítóval", false));
            }
        }

        [Route("user/{userID}/invoice/{orderID}/download")]
        [HttpGet]
        public async Task<ActionResult> DownloadInvoice([FromRoute] string userID, [FromRoute] int orderID)
        {
            if (string.IsNullOrEmpty(userID) == true)
            {
                return BadRequest(new APIResponse<Invoice>(default, "A UserID nem lehet üres", false));
            }

            if (orderID <= 0)
            {
                return BadRequest(new APIResponse<Invoice>(default, "Az OrderID nem lehet kisebb mint 0", false));
            }

            var invoiceFile = await _userInvoicesService.DownloadInvoice(orderID);

            if (invoiceFile != default)
            {
                var result = await _storageService.Download(invoiceFile.FileUri);

                if (result.Success)
                {
                    return File(result.File, "application/pdf");
                }
            }

            return NotFound();
        }
    }
}