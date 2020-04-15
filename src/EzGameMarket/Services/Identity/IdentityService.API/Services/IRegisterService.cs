using IdentityService.API.Models.IdentityModels;
using IdentityService.API.Models.IdentityViewModels;
using System.Threading.Tasks;

namespace IdentityService.API.Services
{
    public interface IRegisterService : IIdentityProvider
    {
        Task<RegisterResult> RegisterAsync(RegisterServiceModel model);

        Task<bool> SendVerification(RegisterVerificationModel model);
    }
}