using Addresses.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Addresses.API.Data
{
    public class AddressesDbContext : DbContext
    {
        public AddressesDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AddressModel> Addresses { get; set; }

        public DbSet<UserAddresses> UserAddresses { get; set; }
    }
}
