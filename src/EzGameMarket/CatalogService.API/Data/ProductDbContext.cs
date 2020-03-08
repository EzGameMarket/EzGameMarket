using CatalogService.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.Data
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ImgSize> ImgSizes { get; set; }

        public ProductDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }
    }
}
