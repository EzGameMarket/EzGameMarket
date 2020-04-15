namespace IdentityService.API.Models.IdentityModels
{
    public class LoginResult
    {
        public AppUser NewUser { get; set; }
        public string Token { get; set; }
    }
}