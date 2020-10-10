using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Shared.Exceptions;
using MarketingService.API.Exceptions.Domain.Campaign;
using MarketingService.API.Exceptions.Model.Campaign;
using MarketingService.API.Services.Repositories.Abstractions;
using MarketingService.API.Services.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketingService.API.Controllers
{
    [Route("api/campaign/manager")]
    [ApiController]
    [Authorize]
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
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _campaignService.PublishAsync(id);

                return Ok();
            }
            catch (CampaignNotFoundException)
            {
                return NotFound();
            }
            catch (CampaignAlreadyPublishedException)
            {
                return Conflict();
            }
            catch (IntegrationEventPublishErrorException)
            {
                return UnprocessableEntity();
            }
        }

        [HttpGet]
        [Route("start/{id}")]
        public async Task<ActionResult> Start([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _campaignService.StartAsync(id);

                return Ok();
            }
            catch (CampaignNotFoundException)
            {
                return NotFound();
            }
            catch (CampaignNotPublishedYetException)
            {
                return NoContent(); 
            }
            catch (CampaignAlreadyStartedException)
            {
                return Conflict();
            }
            catch (IntegrationEventPublishErrorException)
            {
                return UnprocessableEntity();
            }
        }

        [HttpGet]
        [Route("cancel/{id}")]
        public async Task<ActionResult> Cancel([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _campaignService.CancelAsync(id);

                return Ok();
            }
            catch (CampaignNotFoundException)
            {
                return NotFound();
            }
            catch (CampaignAlreadyCanceledException)
            {
                return Conflict();
            }
            catch (IntegrationEventPublishErrorException)
            {
                return UnprocessableEntity();
            }
            catch (Exception ex) when(ex is CampaignNotPublishedYetException || ex is CampaignNotStartedYetException)
            {
                return NoContent();
            }
        }

        [HttpGet]
        [Route("delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _campaignService.DeleteAsync(id);

                return Ok();
            }
            catch (CampaignNotFoundException)
            {
                return NotFound();
            }
            catch (CampaignAlreadyDeletedException)
            {
                return Conflict();
            }
            catch (IntegrationEventPublishErrorException)
            {
                return UnprocessableEntity();
            }
            catch (CampaignAlreadyStartedException)
            {
                return NoContent();
            }
        }
    }
}