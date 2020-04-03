using CloudGamingSupport.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudGamingSupport.API.Data
{
    public class CGDbContext : DbContext
    {
        public CGDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CloudGamingProvider> Providers { get; set; }
        public DbSet<CloudGamingSupported> Games { get; set; }
        public DbSet<CloudGamingProvidersAndGames> Matches { get; set; }
    }
}
