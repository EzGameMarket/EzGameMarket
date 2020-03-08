using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Services
{
    public interface IIdentityProvider
    {
        IIdentityService IdentityService { get; set; }
    }
}
