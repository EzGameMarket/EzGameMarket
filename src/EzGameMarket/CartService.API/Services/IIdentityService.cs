using System.Security.Claims;

namespace CartService.API.Services
{
    public interface IIdentityService
    {
        public string GetUserID(ClaimsPrincipal user);

        public string GetUserName(ClaimsPrincipal user);
    }
}