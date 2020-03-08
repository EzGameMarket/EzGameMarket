using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IdentityService.API.Exceptions;
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
        private readonly ILoginService _loginService;

        public TokenController(IIdentityService identityService,
            ILogger<TokenController> logger,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILoginService loginService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _loginService = loginService;
        }
        
        [HttpPost("generate")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<string> Generate([FromBody] LoginServiceModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var loginRes = await _loginService.LoginAsync(loginModel);

                try
                {
                    var loginResult = await _loginService.LoginAsync(loginModel);

                    HttpContext.Response.Cookies.Append("access_token", loginResult.Token, new CookieOptions() { HttpOnly = true, Secure = true });

                    return loginRes.Token;
                }
                catch (AccountRequire2FAException)
                {
                    return "2FA authentication required before generating the token";
                }
                catch (AccountLockedOutException)
                {
                    return "Your account is locked out";
                }
                catch (InvalidLoginDataException)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return "Invalid Login Data";
                }
            }

            return "Invalid Model";
        }

        [HttpPost("generate2fa")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<string> Generate2FA([FromBody] Login2FAServiceModel loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var loginRes = await _loginService.LoginWith2FAAsync(loginModel, true);

                    return loginRes.Token;
                }
                catch (AccountLockedOutException)
                {
                    return "Your account is locked out";
                }
                catch (InvalidAuthenticatorCodeException)
                {
                    return "Invalid Authenticator Code";
                }
            }

            return "Invalid Model";
        }
    }
}