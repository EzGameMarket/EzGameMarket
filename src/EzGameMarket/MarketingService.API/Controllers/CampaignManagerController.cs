using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketingService.API.Services.Repositories.Abstractions;
using MarketingService.API.Services.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketingService.API.Controllers
{
    [Route("api/campaign/manager")]
    [ApiController]
    public class CampaignManagerController : ControllerBase
    {
        private ICampaignRepository _campaignRepository;
        private ICampaignService _campaignService;

        public CampaignManagerController(ICampaignRepository campaignRepository, ICampaignService campaignService)
        {
            _campaignRepository = campaignRepository;
            _campaignService = campaignService;
        }

        [HttpGet]
        [Route("publish/{id}")]
        public async Task<ActionResult> Publish([FromRoute] int id)
        {
            return default;
        }

        [HttpGet]
        [Route("start/{id}")]
        public async Task<ActionResult> Start([FromRoute] int id)
        {
            return default;
        }

        [HttpGet]
        [Route("cancel/{id}")]
        public async Task<ActionResult> Cancel([FromRoute] int id)
        {
            return default;
        }

        [HttpGet]
        [Route("delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            return default;
        }
    }
}