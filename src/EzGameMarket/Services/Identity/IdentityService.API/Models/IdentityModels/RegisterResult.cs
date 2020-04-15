using Microsoft.AspNetCore.Identity;

namespace IdentityService.API.Models.IdentityModels
{
    public class RegisterResult
    {
        public AppUser NewUser { get; set; }
        public IdentityResult Result { get; set; }
    }
}