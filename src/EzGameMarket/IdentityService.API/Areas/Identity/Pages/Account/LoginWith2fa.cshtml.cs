using IdentityService.API.Exceptions;
using IdentityService.API.Models;
using IdentityService.API.Models.IdentityViewModels;
using IdentityService.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace IdentityService.API.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginWith2faModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LoginWith2faModel> _logger;
        private readonly ILoginService _loginService;

        public LoginWith2faModel(SignInManager<AppUser> signInManager, ILogger<LoginWith2faModel> logger, ILoginService loginService)
        {
            _signInManager = signInManager;
            _logger = logger;
            _loginService = loginService;
        }

        [BindProperty]
        public Login2FAServiceModel Input { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(bool rememberMe, string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            ReturnUrl = returnUrl;
            RememberMe = rememberMe;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            try
            {
                var loginRes = await _loginService.LoginWith2FAAsync(Input, rememberMe);

                return LocalRedirect(returnUrl);
            }
            catch (AccountLockedOutException)
            {
                return RedirectToPage("./Lockout");
            }
            catch (InvalidAuthenticatorCodeException)
            {
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return Page();
            }
        }
    }
}