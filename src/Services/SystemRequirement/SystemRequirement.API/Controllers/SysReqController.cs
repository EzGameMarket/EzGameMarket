using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemRequirement.API.Exceptions;
using SystemRequirement.API.Models;
using SystemRequirement.API.Services.Repositories.Abstractions;

namespace SystemRequirement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysReqController : ControllerBase
    {
        private ISysRequirementsRepository _sysRequirementsRepository;

        public SysReqController(ISysRequirementsRepository sysRequirementsRepository)
        {
            _sysRequirementsRepository = sysRequirementsRepository;
        }

        [HttpGet]
        [Route("product/{productID}")]
        public async Task<ActionResult<List<SysReq>>> GetSysReqsForProductAsync(string productID)
        {
            if (string.IsNullOrEmpty(productID))
            {
                return BadRequest();
            }

            var products = await _sysRequirementsRepository.GetSysReqsForProductAsync(productID);

            if (products != default && products.Count > 0)
            {
                return products;
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<ActionResult<List<SysReq>>> GetAllSysReq()
        {
            var items = await _sysRequirementsRepository.GetAllSysReqAsync();

            return items;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<SysReq>> GetSysReq(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var product = await _sysRequirementsRepository.GetSysReqAsync(id);

            if (product != default)
            {
                return product;
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<ActionResult> Update(int sysReqID, SysReq model)
        {
            if (sysReqID < 1 || ModelState.IsValid == false)
            {
                return BadRequest();
            }

            try
            {
                await _sysRequirementsRepository.Update(sysReqID,model);

                return Ok();
            }
            catch (SysReqNotFoundException)
            {
                return NotFound();
            }
        }

        public async Task<ActionResult> Add(SysReq model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            try
            {
                await _sysRequirementsRepository.Add(model);

                return Ok();
            }
            catch (SysReqAlreadyUploadException)
            {
                return Conflict();
            }
        }
    }
}