using IdentityService.API.Data;
using IdentityService.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.API.Services
{
    public class IdentityProvider : IIdentityService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<IdentityProvider> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public IdentityProvider(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            ILogger<IdentityProvider> logger,
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context;
            _config = configuration;
        }

        public async Task<string> CreateToken(AppUser user)
        {
            var claims = await GetUserClaims(user);

            var token = new JwtSecurityToken(new JwtHeader(
                new SigningCredentials(
                     new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("Secret"))),
                     SecurityAlgorithms.HmacSha256
                    )),

                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IEnumerable<Claim>> GetUserClaims(AppUser user)
        {
            var output = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.AuthenticationMethod,"Identity.Application"),
                new Claim(ClaimTypes.Authentication,"Identity.Application"),
                new Claim(ClaimTypes.AuthorizationDecision,"Identity.Application"),
                new Claim(ClaimTypes.AuthenticationInstant,"Identity.Application"),
                new Claim(JwtRegisteredClaimNames.Typ,"Identity.Application"),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Nbf,new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,new DateTimeOffset(DateTime.Now).AddDays(1).ToUnixTimeSeconds().ToString()),
                
                
            };

            if (_context.Roles != default && _context.UserRoles != default)
            {
                await Task.Run(() =>
                {
                    var roles = from r in _context.Roles
                                join ur in _context.UserRoles on r.Id equals ur.RoleId
                                where ur.UserId == user.Id
                                select r;

                    foreach (var role in roles)
                    {
                        output.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                });
            }

            return output;
        }
    }
}
