using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Shared.Exceptions;
using MarketingService.API.Exceptions.Domain.NewsletterPublisher;
using MarketingService.API.Exceptions.Model.Newsletter;
using MarketingService.API.Extensions;
using MarketingService.API.Services.Repositories.Abstractions;
using MarketingService.API.Services.Services.Abstractions;
using MarketingService.API.ViewModels.NewsletterPublish;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketingService.API.Controllers
{
    [Route("api/newsletter/publisher")]
    [ApiController]
    public class NewsletterPublisherController : ControllerBase
    {
        private INewsletterRepository _newsletterRepository;
        private INewsletterPublisherService _newsletterPublisherService;

        public NewsletterPublisherController(INewsletterRepository newsletterRepository,
                                             INewsletterPublisherService newsletterPublisherService)
        {
            _newsletterRepository = newsletterRepository;
            _newsletterPublisherService = newsletterPublisherService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Publish([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _newsletterPublisherService.SendMailsToAllAsync(id);

                return Ok();
            }
            catch (NewsletterNotFoundException)
            {
                return NotFound();
            }
            catch (IntegrationEventPublishErrorException)
            {
                return UnprocessableEntity();
            }
        }

        [HttpPost]
        [Route("at/")]
        public async Task<ActionResult> PublishAt([FromBody] PublishAtSpecifiedTimeViewModel model)
        {
            if (ModelState.IsValid == false || model.ID <= 0)
            {
                return BadRequest();
            }

            if (model.Time.ToUniversalTime() < DateTime.UtcNow)
            {
                return NoContent();
            }

            try
            {
                await _newsletterPublisherService.SetModelToSendAtSpecificTime(model);

                return Ok();
            }
            catch (NewsletterNotFoundException)
            {
                return NotFound();
            }
            catch (IntegrationEventPublishErrorException)
            {
                return UnprocessableEntity();
            }
        }

        [HttpPost]
        [Route("to/")]
        public async Task<ActionResult> PublishTo([FromBody] PublishToEmailsViewModel model)
        {
            if (ModelState.IsValid == false || model.ID <= 0)
            {
                return BadRequest();
            }

            try
            {
                ValidateEmails(model.Emails);

                await _newsletterPublisherService.SendMailsAsync(model);

                return Ok();
            }
            catch (InValidEmailAddressesException ex)
            {
                return BadRequest(ex.Emails);
            }
            catch (NewsletterNotFoundException)
            {
                return NotFound();
            }
            catch (IntegrationEventPublishErrorException)
            {
                return UnprocessableEntity();
            }
        }

        [HttpPost]
        [Route("to/at/")]
        public async Task<ActionResult> PublishAtTo([FromBody] PublishToEmailsAtSpecifiedTimeViewModel model)
        {
            if (ModelState.IsValid == false || model.ID <= 0)
            {
                return BadRequest();
            }

            if (model.Time.ToUniversalTime() < DateTime.UtcNow)
            {
                return NoContent();
            }

            try
            {
                ValidateEmails(model.Emails);

                await _newsletterPublisherService.SetModelToSendAtSpecificTimeToTheSpecificEmails(model);

                return Ok();
            }
            catch (InValidEmailAddressesException ex)
            {
                return BadRequest(ex.Emails);
            }
            catch (NewsletterNotFoundException)
            {
                return NotFound();
            }
            catch (IntegrationEventPublishErrorException)
            {
                return UnprocessableEntity();
            }
        }

        private void ValidateEmails(IEnumerable<string> addresses)
        {
            var invalidEmails = new List<string>();

            Parallel.ForEach(addresses, (address)=>{

                if (address.IsValidEmail() == false)
                {
                    invalidEmails.Add(address);
                }

            });

            if (invalidEmails.Count > 0)
            {
                throw new InValidEmailAddressesException() { Emails = invalidEmails } ;
            }
        }
    }
}