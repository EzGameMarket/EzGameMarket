using IdentityService.API.Exceptions;
using IdentityService.API.Models;
using IdentityService.API.Models.IdentityModels;
using IdentityService.API.Models.IdentityViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Services
{
    public class LoginProvider : ILoginService
    {
        public IIdentityService IdentityService { get; set; }
        private ILogger<LoginProvider> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginProvider(IIdentityService identityService, ILogger<LoginProvider> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            IdentityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public async Task<LoginResult> LoginAsync(LoginServiceModel model)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true

            var user = await _userManager.FindByEmailAsync(model.Email);

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");

                var token = await IdentityService.CreateToken(user);

                return new LoginResult() { NewUser = user, Token = token };
            }
            if (result.RequiresTwoFactor)
            {
                throw new AccountRequire2FAException();
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                throw new AccountLockedOutException();
            }
            else
            {
                throw new InvalidLoginDataException();
            }
        }

        public async Task<LoginResult> LoginWith2FAAsync(Login2FAServiceModel model, bool rememberMe)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            var authenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.RememberMachine);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID '{UserId}' logged in with 2fa.", user.Id);

                var token = await IdentityService.CreateToken(user);

                return new LoginResult() { NewUser = user, Token = token };
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID '{UserId}' account locked out.", user.Id);
                throw new AccountLockedOutException();
            }
            else
            {
                _logger.LogWarning("Invalid authenticator code entered for user with ID '{UserId}'.", user.Id);
                throw new InvalidAuthenticatorCodeException();
            }
        }
    }
}
