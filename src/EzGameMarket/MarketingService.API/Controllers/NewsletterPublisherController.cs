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
    [Route("api/newsletter/publisher")]
    [ApiController]
    public class NewsletterPublisherController : ControllerBase
    {
        private INewsletterPublisherService _newsletterPublisherService;
        private INewsletterRepository _newsletterRepository;

        public NewsletterPublisherController(INewsletterPublisherService newsletterPublisherService,
                                             INewsletterRepository newsletterRepository)
        {
            _newsletterPublisherService = newsletterPublisherService;
            _newsletterRepository = newsletterRepository;
        }


    }
}