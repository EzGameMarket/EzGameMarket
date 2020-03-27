using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Gtw.Models.ViewModels;

namespace Web.Gtw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        public ActionResult Update(CartItemUpdateModel model)
        {
            
        }
    }
}