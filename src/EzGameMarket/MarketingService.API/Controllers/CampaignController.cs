using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private ICampaignService _campaignService;

        public CampaignController(ICampaignRepository campaignRepository, ICampaignService campaignService)
        {
            _campaignRepository = campaignRepository;
            _campaignService = campaignService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Campaign>> Get([FromRoute] int id)
        {
            return default;
        }

        [HttpGet]
        [Route("title/{name}")]
        public async Task<ActionResult<Campaign>> GetByTitle([FromRoute] string name)
        {
            return default;
        }

        [HttpGet]
        [Route("running/{date}")]
        public async Task<ActionResult<List<Campaign>>> GetRunningCampaigns([FromRoute] DateTime date)
        {
            return default;
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Add([FromBody] Campaign date)
        {
            return default;
        }

        [HttpPost]
        [Route("modify/{id}")]
        public async Task<ActionResult> Add([FromRoute] int id, [FromBody] Campaign date)
        {
            return default;
        }
    }
}