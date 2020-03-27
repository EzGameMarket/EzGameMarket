using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebMVC.Services.Services.Abstractions
{
    public interface IIdentityService
    {
        string GetUserID(ClaimsPrincipal user);
        string GetUserName(ClaimsPrincipal user);
    }
}
