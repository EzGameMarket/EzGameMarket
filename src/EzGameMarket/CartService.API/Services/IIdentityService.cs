using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CartService.API.Services
{
    public interface IIdentityService
    {
        public string GetUserID(ClaimsPrincipal user);
        public string GetUserName(ClaimsPrincipal user);
    }
}
