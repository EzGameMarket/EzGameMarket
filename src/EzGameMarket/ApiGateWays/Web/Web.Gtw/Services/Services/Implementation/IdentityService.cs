using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Gtw.Services.Services.Abstractions;

namespace Web.Gtw.Services.Services.Implementation
{
    public class IdentityService : IIdentityService
    {
        public string GetUserID(ClaimsPrincipal user)
        {
            if (user != default && user.Identity != default)
            {
                return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            }

            return default;
        }

        public string GetUserName(ClaimsPrincipal user)
        {
            if (user != default && user.Identity != default)
            {
                return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            }

            return default;
        }
    }
}
