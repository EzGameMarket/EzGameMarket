using MarketingService.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.Data
{
    public class MarketingDbContext : DbContext
    {
        public MarketingDbContext( DbContextOptions options) : base(options)
        {
        }

        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
    }
}
