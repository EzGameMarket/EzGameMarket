using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Models.IdentityViewModels
{
    public class TokenLoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
