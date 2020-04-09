using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CouponCode.API.Services.Service.Abstractions
{
    public interface IIdentityService
    {
        public string GetUserID(ClaimsPrincipal user);

        public string GetUserName(ClaimsPrincipal user);
    }
}
