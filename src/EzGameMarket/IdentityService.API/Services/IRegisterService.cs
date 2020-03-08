using IdentityService.API.Models;
using IdentityService.API.Models.IdentityModels;
using IdentityService.API.Models.IdentityViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Services
{
    public interface IRegisterService : IIdentityProvider
    {
        Task<RegisterResult> RegisterAsync(RegisterServiceModel model);

        Task<bool> SendVerification(RegisterVerificationModel model);
    }
}
