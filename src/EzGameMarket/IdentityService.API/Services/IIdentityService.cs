using IdentityService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityService.API.Services
{
    public interface IIdentityService
    {
        Task<IEnumerable<Claim>> GetUserClaims(AppUser user);
        public Task<string> CreateToken(AppUser user);
    }
}
