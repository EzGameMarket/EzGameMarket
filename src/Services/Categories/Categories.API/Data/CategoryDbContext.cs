using Categories.API.Controllers;
using Categories.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Categories.API.Data
{
    public class CategoryDbContext : DbContext
    {
        public CategoryDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagsAndProductsLink> ProductTags { get; set; }
        public DbSet<CategoriesAndProductsLink> ProductCategories { get; set; }

        public void Add(object categories)
        {
            throw new NotImplementedException();
        }
    }
}
