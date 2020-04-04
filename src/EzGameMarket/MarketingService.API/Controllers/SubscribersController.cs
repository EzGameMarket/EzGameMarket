using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketingService.API.Models;
using MarketingService.API.Exceptions.Model.Subscribe;
using MarketingService.API.Services.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketingService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController : ControllerBase
    {
        private ISubscriberRepository _subscriberRepository;

        public SubscribersController(ISubscriberRepository subscriberRepository)
        {
            _subscriberRepository = subscriberRepository;
        }

        [HttpGet]
        [Route("add")]
        public async Task<ActionResult> Add(SubscribedMember model)
        {
            if (string.IsNullOrEmpty(model.EMail) || ModelState.IsValid == false)
            {
                return BadRequest();
            }

            try
            {
                await _subscriberRepository.Add(model);

                return Ok();
            }
            catch (SubscribeAlreadyInDbException)
            {
                return Conflict();
            }
        }

        [HttpGet]
        [Route("modify/{id}")]
        public async Task<ActionResult> Modify([FromRoute] int id, SubscribedMember model)
        {
            if (id <= 0 || ModelState.IsValid == false)
            {
                return BadRequest();
            }

            try
            {
                await _subscriberRepository.Modify(id, model);

                return Ok();
            }
            catch (SubscriberMemberNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("email/{email}")]
        public async Task<ActionResult<SubscribedMember>> GetByEmail([FromRoute] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }

            var member = await _subscriberRepository.GetByEmail(email);

            if (member != default)
            {
                return member;
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<SubscribedMember>> GetByID([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var member = await _subscriberRepository.Get(id);

            if (member != default)
            {
                return member;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<SubscribedMember>>> GetBeetwen([FromQuery] DateTime start, [FromQuery] DateTime end = default, [FromQuery] bool active = true)
        {
            if (end == default)
            {
                end = DateTime.Now;
            }

            var members = await _subscriberRepository.GetBeetwen(start,end,active);

            if (members != default && members.Count > 0)
            {
                return members;
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Lekérdezi az összes aktív felhasználót aki felvan iratkozva a newslettterre
        /// </summary>
        /// <returns>List of active newsletter subscribers</returns>
        [HttpGet]
        [Route("actives")]
        public async Task<ActionResult<List<SubscribedMember>>> GetActiveSubscribers()
        {
            var members = await _subscriberRepository.GetActiveMembers();

            if (members != default && members.Count > 0)
            {
                return members;
            }
            else
            {
                return NotFound();
            }
        }
    }
}