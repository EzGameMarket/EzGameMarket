using IdentityService.API.Models.IdentityViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Models.IdentityModels
{
    public class RegisterVerificationModel
    {
        public IUrlHelper Url { get; set; }
        public HttpRequest Request { get; set; }

        public RegisterServiceModel Model { get; set; }
        public AppUser User { get; set; }
    }
}