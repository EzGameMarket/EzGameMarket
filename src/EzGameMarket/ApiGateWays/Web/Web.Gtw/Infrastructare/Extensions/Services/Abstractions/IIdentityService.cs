using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Gtw.Infrastructare.Extensions.Services.Abstractions
{
    public interface IIdentityService
    {
        public string GetUserID(ClaimsPrincipal user);

        public string GetUserName(ClaimsPrincipal user);
    }
}
