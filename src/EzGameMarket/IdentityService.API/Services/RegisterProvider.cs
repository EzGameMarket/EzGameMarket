using IdentityService.API.Exceptions;
using IdentityService.API.Models;
using IdentityService.API.Models.IdentityModels;
using IdentityService.API.Models.IdentityViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace IdentityService.API.Services
{
    public class RegisterProvider : IRegisterService
    {
        public IIdentityService IdentityService { get; set; }
        private ILogger<RegisterProvider> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public RegisterProvider(IIdentityService identityService,
            ILogger<RegisterProvider> logger,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IEmailSender emailSender)
        {
            IdentityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }

        private async Task<bool> ValidateUserName(RegisterServiceModel model)
        {
            var userWithUserName = await _userManager.FindByNameAsync(model.Email);

            return userWithUserName != default;
        }

        private async Task<bool> ValidateEmail(RegisterServiceModel model)
        {
            var userWithEmail = await _userManager.FindByEmailAsync(model.Email);

            return userWithEmail != default;
        }

        public async Task<RegisterResult> RegisterAsync(RegisterServiceModel model)
        {
            if (await ValidateUserName(model))
            {
                _logger.LogInformation($"There is a user registered with the {model.UserName} username");
                throw new UsernameAlreadyRegisteredException($"There is a user registered with the {model.UserName} username") { UserName = model.UserName } ;
            }

            if (await ValidateEmail(model))
            {
                _logger.LogInformation($"There is a user registered with the {model.Email} email");
                throw new EmailAlreadyRegisteredException($"There is a user registered with the {model.Email} email") { Email = model.Email };
            }

            var user = new AppUser { UserName = model.UserName, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            return new RegisterResult() { NewUser = user, Result = result };
        }

        public async Task<bool> SendVerification(RegisterVerificationModel model)
        {
            if (model != default)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(model.User);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = model.Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = model.User.Id, code = code },
                    protocol: model.Request.Scheme);

                await _emailSender.SendEmailAsync(model.User.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return true; 
            }
            else
            {
                return false;
            }
        }
    }
}
