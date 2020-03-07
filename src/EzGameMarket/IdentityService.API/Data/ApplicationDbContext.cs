using System;
using System.Collections.Generic;
using System.Text;
using IdentityService.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        DbSet<Address> Addresses;
        DbSet<PromoCode> Codes;
        DbSet<CreditCard> Cards;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
