using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IdentityService.API.Models;
using IdentityService.API.Models.IdentityViewModels;
using IdentityService.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdentityService.API.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<TokenController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public TokenController(IIdentityService identityService, ILogger<TokenController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }
        
        [HttpPost("generate")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<string> Generate([FromBody] TokenLoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password,true,false);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"{loginModel.UserName} successfully generated a token");
                    var user = await _userManager.FindByNameAsync(loginModel.UserName);

                    var token = await _identityService.CreateToken(user);

                    HttpContext.Response.Cookies.Append("access_token",token, new CookieOptions() { HttpOnly = true, Secure = true});

                    return token;
                }
                if (result.RequiresTwoFactor)
                {
                    return "2FA authentication required before generating the token";
                }
                if (result.IsLockedOut)
                {
                    return "Your account is locked out";
                }
                if (result.IsNotAllowed)
                {
                    return "Access Denied";
                }
            }

            return "Invalid Model";
        }

        [HttpPost("generate2fa")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<string> Generate2FA([FromBody] TokenLoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, true, false);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"{loginModel.UserName} successfully generated a token");
                    var user = await _userManager.FindByNameAsync(loginModel.UserName);
                    return await _identityService.CreateToken(user);
                }
                if (result.IsLockedOut)
                {
                    return "Your account is locked out";
                }
                if (result.IsNotAllowed)
                {
                    return "Access Denied";
                }
            }

            return "Invalid Model";
        }
    }
}