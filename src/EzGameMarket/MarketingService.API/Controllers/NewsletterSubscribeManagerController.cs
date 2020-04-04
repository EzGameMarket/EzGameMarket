using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketingService.API.Models;
using MarketingService.API.Services.Repositories.Abstractions;
using MarketingService.API.Services.Services.Abstractions;
using MarketingService.API.ViewModels.Subscribe;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketingService.API.Controllers
{
    [Route("api/newsletter/manage")]
    [ApiController]
    public class NewsletterSubscribeManagerController : ControllerBase
    {
        private ISubscriberRepository _subscriberRepository;
        private ISubscribeManagerService _subscribeManagerService;

        public NewsletterSubscribeManagerController(ISubscriberRepository subscriberRepository,
                                                    ISubscribeManagerService subscribeManagerService)
        {
            _subscriberRepository = subscriberRepository;
            _subscribeManagerService = subscribeManagerService;
        }

        [HttpGet]
        [Route("subscribe")]
        public async Task<ActionResult> Subscribe([FromBody] SubscribeViewModel model)
        {
            if (string.IsNullOrEmpty(model.EMail) || ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var member = await _subscriberRepository.GetByEmail(model.EMail);

            if (member == default)
            {
                var newMember = new SubscribedMember()
                {
                    ID = default,
                    Active = true,
                    EMail = model.EMail,
                    SubscribedDate = DateTime.Now,
                    UnSubscribedDate = default
                };

                await _subscriberRepository.Add(newMember);

                return Ok();
            }

            await _subscribeManagerService.Subscribe(member);

            return Accepted();
        }

        [HttpGet]
        [Route("unsubscribe")]
        public async Task<ActionResult> UnSubscribe([FromBody] SubscribeViewModel model)
        {
            if (string.IsNullOrEmpty(model.EMail) || ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var member = await _subscriberRepository.GetByEmail(model.EMail);

            if (member == default)
            {
                return NotFound();
            }

            await _subscribeManagerService.UnSubscribe(member);

            return Accepted();
        }
    }
}