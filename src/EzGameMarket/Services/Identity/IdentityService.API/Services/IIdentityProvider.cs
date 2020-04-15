namespace IdentityService.API.Services
{
    public interface IIdentityProvider
    {
        IIdentityService IdentityService { get; set; }
    }
}