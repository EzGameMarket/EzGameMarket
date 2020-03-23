using IdentityService.API.Exceptions;
using IdentityService.API.Models;
using IdentityService.API.Models.IdentityModels;
using IdentityService.API.Models.IdentityViewModels;
using IdentityService.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace IdentityService.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<IdentityController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IRegisterService _registerService;
        private readonly IIdentityService _identityService;
        private readonly ILoginService _loginService;

        public IdentityController(SignInManager<AppUser> signInManager,
            ILogger<IdentityController> logger,
            UserManager<AppUser> userManager,
            IEmailSender emailSender,
            IRegisterService registerService,
            IIdentityService identityService,
            ILoginService loginService)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            _registerService = registerService ?? throw new ArgumentNullException(nameof(registerService));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _loginService = loginService;
        }

        [HttpPost("logout")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            HttpContext.Response.Cookies.Delete("access_token");

            return Ok();
        }

        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginServiceModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var loginResult = await _loginService.LoginAsync(model);

                    HttpContext.Response.Cookies.Append("access_token", loginResult.Token, new CookieOptions() { HttpOnly = true, Secure = true });

                    return Ok(loginResult.Token);
                }
                catch (AccountRequire2FAException)
                {
                    return Unauthorized("2FA authentication required before generating the token");
                }
                catch (AccountLockedOutException)
                {
                    return BadRequest("Your account is locked out");
                }
                catch (InvalidLoginDataException)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest("Invalid Login Data");
                }
            }

            return BadRequest("Invalid Model");
        }

        [HttpPost("login2FA")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login2FA([FromBody] Login2FAServiceModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var loginRes = await _loginService.LoginWith2FAAsync(model, true);

                    return Ok(loginRes.Token);
                }
                catch (AccountLockedOutException)
                {
                    return BadRequest("Your account is locked out");
                }
                catch (InvalidAuthenticatorCodeException)
                {
                    return BadRequest("Invalid Authenticator Code");
                }
            }

            return BadRequest("Invalid Model");
        }

        [HttpPost("register")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterServiceModel model)
        {
            var regResult = await _registerService.RegisterAsync(model);

            await _registerService.SendVerification(new RegisterVerificationModel() { Model = model, Request = Request, Url = Url, User = regResult.NewUser });

            if (regResult.Result.Succeeded)
            {
                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    return Ok("Confirmation Needed");
                }
                else
                {
                    await _signInManager.SignInAsync(regResult.NewUser, isPersistent: false);

                    var token = await _identityService.CreateToken(regResult.NewUser);

                    HttpContext.Response.Cookies.Append("access_token", token, new CookieOptions() { HttpOnly = true, Secure = true });

                    return Ok(regResult);
                }
            }
            else
            {
                return BadRequest($"Registration failed");
            }
        }
    }
}