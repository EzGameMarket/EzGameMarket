using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogImages.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogImages.API.Data
{
    public class CatalogImagesDbContext : DbContext
    {
        public CatalogImagesDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CatalogItemImageModel> Images { get; set; }

        public DbSet<ImageSizeModel> ImageSizes { get; set; }

        public DbSet<ImageTypeModel> ImageTypes { get; set; }
    }
}
