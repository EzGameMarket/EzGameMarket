using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketingService.API.Exceptions.Model.Campaign;
using MarketingService.API.Models;
using MarketingService.API.Services.Repositories.Abstractions;
using MarketingService.API.Services.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketingService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CampaignController : ControllerBase
    {
        private ICampaignRepository _campaignRepository;

        public CampaignController(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Campaign>> Get([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var campaign = await _campaignRepository.Get(id);

            if (campaign != default)
            {
                return campaign;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("title/{name}")]
        public async Task<ActionResult<Campaign>> GetByTitle([FromRoute] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            var campaign = await _campaignRepository.GetByCampaignTitle(name);

            if (campaign != default)
            {
                return campaign;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("running/{from}")]
        public async Task<ActionResult<List<Campaign>>> GetRunningCampaigns([FromRoute] DateTime from = default)
        {
            if (from == default)
            {
                from = DateTime.Now;
            }

            var campaigns = await _campaignRepository.GetRunningCampaigns(from);

            if (campaigns != default && campaigns.Count > 0)
            {
                return campaigns;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Add([FromBody] Campaign model)
        {
            if (string.IsNullOrEmpty(model.Title) || ModelState.IsValid == false)
            {
                return BadRequest();
            }

            try
            {
                await _campaignRepository.Add(model);

                return Ok();
            }
            catch (Exception ex) when (ex is CampaignAlreadyInDbException || ex is CampaignWithTitleAlreadInDbException)
            {
                return Conflict();
            }
        }
         
        [HttpPost]
        [Route("modify/{id}")]
        public async Task<ActionResult> Modify([FromRoute] int id, [FromBody] Campaign model)
        {
            if (id <= 0 || string.IsNullOrEmpty(model.Title) || ModelState.IsValid == false)
            {
                return BadRequest();
            }

            try
            {
                await _campaignRepository.Modify(id, model);

                return Ok();
            }
            catch (CampaignNotFoundException)
            {
                return NotFound();
            }
        }
    }
}