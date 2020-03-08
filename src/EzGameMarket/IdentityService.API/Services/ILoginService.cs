using IdentityService.API.Models.IdentityModels;
using IdentityService.API.Models.IdentityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Services
{
    public interface ILoginService : IIdentityProvider
    {
        Task<LoginResult> LoginAsync(LoginServiceModel model);
        Task<LoginResult> LoginWith2FAAsync(Login2FAServiceModel model, bool rememberMe);
    }
}
