using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudGamingSupport.API.Exceptions;
using CloudGamingSupport.API.Models;
using CloudGamingSupport.API.Services.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudGamingSupport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CgProviderController : ControllerBase
    {
        private ICloudGamingProviderRepository _cloudGamingProviderRepository;

        public CgProviderController(ICloudGamingProviderRepository cloudGamingProviderRepository)
        {
            _cloudGamingProviderRepository = cloudGamingProviderRepository;
        }

        [HttpPost]
        [Route("add/")]
        public async Task<ActionResult> Add([FromBody] CloudGamingProvider model)
        {
            if (ModelState.IsValid == false || model.ID <= -1)
            {
                return BadRequest();
            }

            try
            {
                await _cloudGamingProviderRepository.Add(model);

                return Ok();
            }
            catch (CgAlreadyInDataBaseException)
            {
                return Conflict();
            }
        }

        [HttpPost]
        [Route("modify/{id}")]
        public async Task<ActionResult> Modify([FromRoute] int id,[FromBody] CloudGamingProvider model)
        {
            if (id <= 0 || ModelState.IsValid == false)
            {
                return BadRequest();
            }

            try
            {
                await _cloudGamingProviderRepository.Modify(id, model);

                return Ok();
            }
            catch (CgProviderNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CloudGamingProvider>> Get([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var product = await _cloudGamingProviderRepository.Get(id);

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
        [Route("products/{id}")]
        public async Task<ActionResult<List<string>>> GetSupportedProducts([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var products = await _cloudGamingProviderRepository.GetSupportedGames(id);

                if (products != default && products.Count > 0)
                {
                    return products;
                }
            }
            catch (CgNotFoundException) { }

            return NotFound();
        }
    }
}