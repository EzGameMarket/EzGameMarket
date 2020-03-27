using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Gtw.Models.ViewModels.Order;

namespace Web.Gtw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        public Task<OrderDraftViewModel> GetDraft(string cartID)
        {
            return default;
        }
    }
}