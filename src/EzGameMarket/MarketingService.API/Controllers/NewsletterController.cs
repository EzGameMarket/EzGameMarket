using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketingService.API.Models;
using MarketingService.API.Services.Repositories.Abstractions;
using MarketingService.API.Exceptions.Model.Newsletter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketingService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NewsletterController : ControllerBase
    {
        private INewsletterRepository _newsletterRepository;

        public NewsletterController(INewsletterRepository newsletterRepository)
        {
            _newsletterRepository = newsletterRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<NewsletterMessage>> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var newsletter = await _newsletterRepository.Get(id);

            if (newsletter != default)
            {
                return newsletter;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("title/{title}")]
        public async Task<ActionResult<NewsletterMessage>> GetByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest();
            }

            var newsLetter = await _newsletterRepository.GetByTitle(title);

            if (newsLetter != default)
            {
                return newsLetter;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Add([FromBody]NewsletterMessage model)
        {
            if (string.IsNullOrEmpty(model.Title) || ModelState.IsValid == false)
            {
                return BadRequest();
            }

            try
            {
                await _newsletterRepository.Add(model);

                return Ok();
            }
            catch (Exception ex) when (ex is NewslettersWithTitleAlreadyInDbException || ex is NewsletterAlreadyInDbException)
            {
                return Conflict();
            }
        }

        [HttpGet]
        [Route("modify/{id}")]
        public async Task<ActionResult> Modify([FromQuery]int id, [FromBody]NewsletterMessage model)
        {
            if (id <= 0 || ModelState.IsValid == false || string.IsNullOrEmpty(model.Title))
            {
                return BadRequest();
            }

            try
            {
                await _newsletterRepository.Modify(id, model);

                return Ok();
            }
            catch (NewsletterNotFoundException)
            {
                return NotFound();
            }
        }
    }
}