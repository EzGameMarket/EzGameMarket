using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CouponCode.API.Models;
using CouponCode.API.Services.Repositories.Abstractions;
using CouponCode.API.Exceptions.Discount;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CouponCode.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<DiscountModel>> GetByID([FromRoute] int id)
        {
            if (id<= 0)
            {
                return BadRequest();
            }

            var discount = await _discountRepository.GetByID(id);

            if (discount != default)
            {
                return discount;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("name/{name}")]
        public async Task<ActionResult<DiscountModel>> GetByName([FromRoute] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            var discount = await _discountRepository.GetByName(name);

            if (discount != default)
            {
                return discount;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("add/")]
        public async Task<ActionResult> Add([FromBody] DiscountModel model)
        {
            if (model.ID <= 0 || string.IsNullOrEmpty(model.Name) || ModelState.IsValid == false)
            {
                return BadRequest();
            }

            try
            {
                await _discountRepository.Add(model);

                return Ok();
            }
            catch (DiscountAlreadyInDbWithIDException)
            {
                return Conflict("ID");
            }
            catch (DiscountAlreadyInDbWithNameException)
            {
                return Conflict("Name");
            }
            catch (DiscountTooHighException)
            {
                return BadRequest("Discount is too high");
            }
        }

        [HttpGet]
        [Route("modify/{id}")]
        public async Task<ActionResult> Modify([FromRoute] int id, [FromBody] DiscountModel model)
        {
            if (id <= 0 || string.IsNullOrEmpty(model.Name) || ModelState.IsValid == false)
            {
                return BadRequest();
            }

            try
            {
                await _discountRepository.Update(id, model);

                return Ok();
            }
            catch (DiscountNotFoundException)
            {
                return NotFound();
            }
            catch (DiscountTooHighException)
            {
                return BadRequest("Discount is too high");
            }
        }
    }
}