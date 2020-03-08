using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Models.IdentityModels
{
    public class RegisterResult
    {
        public AppUser NewUser { get; set; }
        public IdentityResult Result { get; set; }
    }
}
