using IdentityService.API.Models.IdentityViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
