using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketingService.API.Models;
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
        public async Task<ActionResult> Add()
        {
            return default;
        }

        [HttpGet]
        [Route("modify/{id}")]
        public async Task<ActionResult> Modify([FromRoute] int id)
        {
            return default;
        }

        [HttpGet]
        [Route("email/{email}")]
        public async Task<ActionResult> GetByEmail([FromRoute] string email)
        {
            return default;
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
        public async Task<ActionResult> GetBeetwen([FromQuery] DateTime start, [FromQuery] DateTime end = default, [FromQuery] bool active = true)
        {
            //if (end == default)
            //{
            //    end = DateTime.Now;
            //}

            return default;
        }

        /// <summary>
        /// Lekérdezi az összes aktív felhasználót aki felvan iratkozva a newslettterre
        /// </summary>
        /// <returns>List of active newsletter subscribers</returns>
        [HttpGet]
        [Route("actives")]
        public async Task<ActionResult> GetActiveSubscribers()
        {
            return default;
        }
    }
}