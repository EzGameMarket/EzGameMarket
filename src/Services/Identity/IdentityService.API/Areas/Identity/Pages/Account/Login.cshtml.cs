using IdentityService.API.Exceptions;
using IdentityService.API.Models;
using IdentityService.API.Models.IdentityViewModels;
using IdentityService.API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IIdentityService _identityService;
        private readonly ILoginService _loginService;

        public LoginModel(SignInManager<AppUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<AppUser> userManager,
            IIdentityService identityService,
            ILoginService loginService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _identityService = identityService;
            _loginService = loginService;
        }

        [BindProperty]
        public LoginServiceModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task OnGetAsync([FromRoute] string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                try
                {
                    var loginResult = await _loginService.LoginAsync(Input);

                    HttpContext.Response.Cookies.Append("access_token", loginResult.Token, new CookieOptions() { HttpOnly = true, Secure = true });

                    if (string.IsNullOrEmpty(ReturnUrl) == false)
                    {
                        returnUrl = ReturnUrl;
                    }

                    return LocalRedirect(returnUrl);
                }
                catch (UserNotRegisteredWithEmailYetException)
                {
                    return RedirectToPage("./Register");
                }
                catch (AccountRequire2FAException)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                catch (AccountLockedOutException)
                {
                    return RedirectToPage("./Lockout");
                }
                catch (InvalidLoginDataException)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}