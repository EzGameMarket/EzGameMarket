using IdentityService.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        private DbSet<Address> Addresses;
        private DbSet<PromoCode> Codes;
        private DbSet<CreditCard> Cards;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}