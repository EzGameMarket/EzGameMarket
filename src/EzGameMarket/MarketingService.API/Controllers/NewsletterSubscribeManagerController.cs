using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return default;
        }

        [HttpGet]
        [Route("unsubscribe")]
        public async Task<ActionResult> UnSubscribe([FromBody] SubscribeViewModel model)
        {
            return default;
        }
    }
}