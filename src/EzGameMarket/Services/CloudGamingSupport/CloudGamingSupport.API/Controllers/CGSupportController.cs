using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudGamingSupport.API.Exceptions;
using CloudGamingSupport.API.Models;
using CloudGamingSupport.API.Services.Repositories.Abstractions;
using CloudGamingSupport.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudGamingSupport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CGSupportController : ControllerBase
    {
        private ICloudGamingSupportRepository _cloudGamingSupportRepository;

        public CGSupportController(ICloudGamingSupportRepository cloudGamingSupportRepository)
        {
            _cloudGamingSupportRepository = cloudGamingSupportRepository;
        }

        [HttpGet]
        [Route("product/{productID}")]
        public async Task<ActionResult<CloudGamingSupported>> GetFromProductID([FromRoute] string productID)
        {
            if (string.IsNullOrEmpty(productID))
            {
                return BadRequest();
            }

            var product = await _cloudGamingSupportRepository.GetFromProductID(productID);

            if (product != default)
            {
                return product;
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CloudGamingSupported>> Get([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var product = await _cloudGamingSupportRepository.Get(id);

            if (product != default)
            {
                return product;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("product/add")]
        public async Task<ActionResult> AddProviderForProduct([FromBody]AddProviderForGameViewModel model)
        {
            if (ModelState.IsValid == false || model.ProviderID <= 0 || string.IsNullOrEmpty(model.ProductID))
            {
                return BadRequest();
            }

            try
            {
                await _cloudGamingSupportRepository.AddProviderToGame(model);

                return Ok();
            }
            catch (ProviderAlreadyAddedForProductException)
            {
                return Conflict();
            }
            catch (Exception ex) when (ex is CgNotFoundException || ex is CgProviderNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("add") ]
        public async Task<ActionResult> Add([FromBody] CloudGamingSupported item)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            try
            {
                await _cloudGamingSupportRepository.Add(item);

                return Ok();
            }
            catch (CgAlreadyInDataBaseException)
            {
                return Conflict();
            }
        }

        [HttpPost]
        [Route("modify/{id}")]
        public async Task<ActionResult> Modify([FromRoute]int id, [FromBody] CloudGamingSupported item)
        {
            if (id <= 0 || ModelState.IsValid == false)
            {
                return BadRequest();
            }

            try
            {
                await _cloudGamingSupportRepository.Modify(id, item);

                return Ok();
            }
            catch (CgNotFoundException)
            {
                return NotFound();
            }
        }
    }
}