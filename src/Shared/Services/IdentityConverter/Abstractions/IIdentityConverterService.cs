using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Shared.Services.IdentityConverter.Abstractions
{
    public interface IIdentityConverterService
    {
        public string GetUserID(ClaimsPrincipal user);

        public string GetUserName(ClaimsPrincipal user);
    }
}
