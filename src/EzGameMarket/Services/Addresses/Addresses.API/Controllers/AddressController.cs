using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Addresses.API.Exceptions;
using Addresses.API.Models;
using Addresses.API.Services.Repositories.Abstractions;
using Addresses.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Services.API.Communication.Models;

namespace Addresses.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private IUserAddressRepository _userAddressRepository;

        public AddressController(IUserAddressRepository userAddressRepository)
        {
            _userAddressRepository = userAddressRepository;
        }

        [HttpGet]
        [Route("user/{userID}/default")]
        public async Task<ActionResult<APIResponse<AddressModel>>> GetDefaultAddressForUser([FromRoute] string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return BadRequest();
            }

            var data = await _userAddressRepository.GetDefaultForUser(userID);

            if (data != default)
            {
                return new APIResponse<AddressModel>(data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("user/{userID}")]
        public async Task<ActionResult<APIResponse<List<AddressModel>>>> GetAddressesForUser([FromRoute] string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return BadRequest();
            }

            var data = await _userAddressRepository.GetAddressesForUser(userID);

            if (data != default)
            {
                return new APIResponse<List<AddressModel>>(data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("user/{userID}/setdef")]
        public async Task<ActionResult<EmptyAPIResponse>> SetDeafultAddressForUser([FromRoute] string userID, [FromBody] int addressID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return BadRequest(new EmptyAPIResponse($"A {nameof(userID)} nem lehet üres"));
            }

            if (addressID <= 0)
            {
                return BadRequest(new EmptyAPIResponse("A cím azonosítója nem lehet kisebb mint 1"));
            }

            try
            {
                await _userAddressRepository.SetDefaultAddress(userID, addressID);

                return Ok(new EmptyAPIResponse());
            }
            catch (AddressNotFoundByIDException)
            {
                return NotFound(new EmptyAPIResponse($"A {addressID} azonosítóval nem létezik cím a rendszerben"));
            }
            catch (AddressesNotFoundForUserIDException)
            {
                return NotFound(new EmptyAPIResponse($"A {userID} felhasználónak nincs cím a rendszerben"));
            }
            catch (AddressNotAsignedForUserIDException)
            {
                return Conflict(new EmptyAPIResponse($"A {addressID} azonosítóval a cím nem a {userID} felhasználóhoz van kötve"));
            }

        }

        [HttpGet]
        [Route("address/{addressID}")]
        public async Task<ActionResult<APIResponse<AddressModel>>> GetAddress([FromRoute] int addressID)
        {
            if (addressID <= 0)
            {
                return BadRequest();
            }

            var data = await _userAddressRepository.GetByID(addressID);

            if (data != default)
            {
                return new APIResponse<AddressModel>(data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("address/upload")]
        public async Task<ActionResult<EmptyAPIResponse>> UploadAddress([FromBody] AddNewAddressToUserViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(model.UserID))
            {
                return BadRequest(new EmptyAPIResponse("A UserID nem lehet üres"));
            }

            if (model.NewAddress == default)
            {
                return BadRequest(new EmptyAPIResponse("Az új cím model nem lehet null"));
            }

            try
            {
                await _userAddressRepository.AddAddressForUser(model);

                return Ok(new EmptyAPIResponse());
            }
            catch (AddressAlreadyExistsWithIDException)
            {
                return Conflict(new EmptyAPIResponse($"A {model.NewAddress.ID.GetValueOrDefault()} azonosítóval már létezik cím a rendszerben"));
            }
        }

        [HttpPost]
        [Route("address/{addressID}/modify")]
        public async Task<ActionResult<EmptyAPIResponse>> ModifyAddress([FromRoute] int addressID, [FromBody] AddressModel model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (addressID <= 0)
            {
                return BadRequest(new EmptyAPIResponse("A paraméter cím azonosító nem lehet kisebb mint 1"));
            }

            if (addressID != model.ID.GetValueOrDefault())
            {
                return BadRequest(new EmptyAPIResponse("A model IDja és a parmatér ID nem egyezik"));
            }

            try
            {
                await _userAddressRepository.UpdateAddress(addressID, model);

                return Ok(new EmptyAPIResponse());
            }
            catch (AddressNotFoundByIDException)
            {
                return NotFound(new EmptyAPIResponse("A 100 azonosítóval nem létezik cím a rendszerben"));
            }
        }

        [HttpDelete]
        [Route("{addressID}/delete")]
        public async Task<ActionResult<EmptyAPIResponse>> DeleteAddress([FromRoute] int addressID)
        {
            if (addressID <= 0)
            {
                return BadRequest(new EmptyAPIResponse(false,$"A cím azonosítója nem lehet kisebb mint 1"));
            }

            try
            {
                await _userAddressRepository.DeleteAddress(addressID);

                return Ok(new EmptyAPIResponse(true));
            }
            catch (AddressNotFoundByIDException)
            {
                return NotFound(new EmptyAPIResponse(false, $"A {addressID} azonosítóval nem létezik cím a rendszerben"));
            }
        }
    }
}