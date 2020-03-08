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

        private async Task<bool> ValidateUser(RegisterServiceModel model)
        {
            var userWithUserName = await _userManager.FindByNameAsync(model.UserName);

            return userWithUserName != default;
        }

        public async Task<RegisterResult> RegisterAsync(RegisterServiceModel model)
        {
            if (await ValidateUser(model))
            {
                _logger.LogInformation($"There is a user registered with the {model.UserName} username");
                throw new UsernameAlreadyRegisteredException() { UserName = model.UserName } ;
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
