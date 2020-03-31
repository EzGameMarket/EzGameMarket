using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemRequirement.API.Models;

namespace SystemRequirement.API.Data
{
    public class SysReqDbContext : DbContext
    {
        public SysReqDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SysReq> SysReqs { get; set; }
        public DbSet<CPUNeeds> CPUNeeds { get; set; }
        public DbSet<GPUNeeds> GPUNeeds { get; set; }
        public DbSet<RAMNeeds> RAMNeeds { get; set; }
        public DbSet<StorageNeeds> StorageNeeds { get; set; }
        public DbSet<NetworkNeeds> NetworkNeeds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var converter = new EnumToStringConverter<SysReqType>();

            modelBuilder
                .Entity<SysReq>()
                .Property(e => e.Type)
                .HasConversion(converter);

            //base.OnModelCreating(modelBuilder);
        }
    }
}
