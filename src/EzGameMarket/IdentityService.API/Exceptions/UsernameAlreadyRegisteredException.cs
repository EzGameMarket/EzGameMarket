using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Exceptions
{
    public class UsernameAlreadyRegisteredException : Exception
    {
        public string UserName { get; set; }
    }
}
