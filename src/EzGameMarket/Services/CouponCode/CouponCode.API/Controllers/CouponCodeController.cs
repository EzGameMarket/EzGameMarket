using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CouponCode.API.Exceptions;
using CouponCode.API.Exceptions.CouponCode;
using CouponCode.API.Exceptions.CouponCode.CouponCodeValidation;
using CouponCode.API.Models;
using CouponCode.API.Services.Repositories.Abstractions;
using CouponCode.API.Services.Service.Abstractions;
using CouponCode.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CouponCode.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponCodeController : ControllerBase
    {
        private ICouponCodeRepository _couponCodeRepository;
        private IIdentityService _identityService;

        public CouponCodeController(ICouponCodeRepository couponCodeRepository, IIdentityService identityService)
        {
            _couponCodeRepository = couponCodeRepository;
            _identityService = identityService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CouponCodeModel>> GetByID([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var couponCode = await _couponCodeRepository.GetByID(id);

            if (couponCode != default)
            {
                return couponCode;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("/{couponCode}/add/users")]
        public async Task<ActionResult> AddUsersToCoupon([FromRoute]string couponCode, [FromBody]List<string> userIDs)
        {
            if (string.IsNullOrEmpty(couponCode) || userIDs == default || userIDs.Count == 0)
            {
                return BadRequest();
            }

            try
            {
                await _couponCodeRepository.AddUsersToCoupon(couponCode, userIDs);

                return Ok();
            }
            catch (CouponCodeNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("modify/{id}")]
        public async Task<ActionResult> Modify([FromRoute]int id, [FromBody] CouponCodeModel model)
        {
            if (id <= 0  || string.IsNullOrEmpty(model.Code) || ModelState.IsValid == false)
            {
                return BadRequest();
            }

            try
            {
                await _couponCodeRepository.Update(id, model);

                return Ok();
            }
            catch (CouponCodeWithIDNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("/code/{code}")]
        public async Task<ActionResult<CouponCodeModel>> GetByCode([FromRoute] string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest();
            }

            var couponCode = await _couponCodeRepository.GetByCodeName(code);

            if (couponCode != default)
            {
                return couponCode;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("validate")]
        //Ha a userID null akkor a jelenleg bejelentkezett UserIDt kell használni
        public async Task<ActionResult> ValidateCouponCode([FromBody] ValidateCouponCodeViewModel model)
        {
            if (string.IsNullOrEmpty(model.CouponCode))
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(model.UserID))
            {
                model.UserID = _identityService.GetUserID(User);
            }

            try
            {
                var result = await _couponCodeRepository.Validate(model);

                if (result)
                {
                    return Accepted();
                }
            }
            catch (CouponCodeNotFoundException)
            {
                return NotFound();
            }
            catch (CouponCodeOutdatedException)
            {
                return Conflict();
            }
            catch (UserNotEligibleForCouponCodeException)
            {
                return Forbid();
            }

            return BadRequest();
        }


        [HttpPost]
        [Route("add/")]
        public async Task<ActionResult> Add([FromBody]CouponCodeModel model)
        {
            if (model.ID <= 0 || ModelState.IsValid == false || string.IsNullOrEmpty(model.Code))
            {
                return BadRequest();
            }

            try
            {
                await _couponCodeRepository.Add(model);

                return Ok();
            }
            catch (CouponCodeAlreadyUploadedException)
            {
                return Conflict("CouponCode");
            }
            catch (CouponWithIDAlreadyUploadedException)
            {
                return Conflict("CouponCode ID");
            }
        }
    }
}