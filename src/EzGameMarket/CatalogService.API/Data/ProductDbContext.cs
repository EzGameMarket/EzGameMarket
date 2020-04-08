using CatalogService.API.Models;
using Microsoft.EntityFrameworkCore;

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

        public DbSet<ProductKey> Keys { get; set; }

        public ProductDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}