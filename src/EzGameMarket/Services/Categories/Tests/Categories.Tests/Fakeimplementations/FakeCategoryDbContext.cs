using Categories.API.Data;
using Categories.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Categories.Tests.Fakeimplementations
{
    public class FakeCategoryDbContext
    {
        public CategoryDbContext DbContext { get; set; }
        public DbContextOptions<CategoryDbContext> DbOptions { get; set; }

        public FakeCategoryDbContext()
        {
            var tags = new List<Tag>()
            {
                new Tag()
                {
                    ID = 1,
                    Name = "FPS",
                    Products = new List<TagsAndProductsLink>()
                    {
                        new TagsAndProductsLink()
                        {
                            ID = 1,
                            ProductID = "csgo"
                        },
                        new TagsAndProductsLink()
                        {
                            ID = 2,
                            ProductID = "bfv"
                        },
                        new TagsAndProductsLink()
                        {
                            ID = 3,
                            ProductID = "gtav"
                        },
                    }
                },                
                new Tag()
                {
                    ID = 2,
                    Name = "Sport",
                    Products = new List<TagsAndProductsLink>()
                    {
                        new TagsAndProductsLink()
                        {
                            ID = 4,
                            ProductID = "fifa2020"
                        },
                    }
                },
                new Tag()
                {
                    ID = 3,
                    Name = "Akció",
                    Products = new List<TagsAndProductsLink>()
                    {
                        new TagsAndProductsLink()
                        {
                            ID = 5,
                            ProductID = "csgo"
                        },
                        new TagsAndProductsLink()
                        {
                            ID = 6,
                            ProductID = "bfv"
                        },
                        new TagsAndProductsLink()
                        {
                            ID = 7,
                            ProductID = "gtav"
                        },
                    }
                }
            };

            var categories = new List<Category>()
            {
                new Category()
                {
                    ID = 1,
                    Name = "FPS",
                    Products = new List<CategoriesAndProductsLink>()
                    {
                        new CategoriesAndProductsLink()
                        {
                            ID = 1,
                            ProductID = "csgo",
                        },
                        new CategoriesAndProductsLink()
                        {
                            ID = 2,
                            ProductID = "bfv",
                        },
                    }
                },
                new Category()
                {
                    ID = 2,
                    Name = "sport",
                    Products = new List<CategoriesAndProductsLink>()
                    {
                        new CategoriesAndProductsLink()
                        {
                            ID = 3,
                            ProductID = "fifa2020",
                        },
                        new CategoriesAndProductsLink()
                        {
                            ID = 4,
                            ProductID = "pes2020",
                        },
                    }
                },new Category()
                {
                    ID = 3,
                    Name = "Akció",
                    Products = new List<CategoriesAndProductsLink>()
                    {
                        new CategoriesAndProductsLink()
                        {
                            ID = 5,
                            ProductID = "csgo",
                        },
                        new CategoriesAndProductsLink()
                        {
                            ID = 6,
                            ProductID = "bfv",
                        },
                    }
                },
            };

            DbOptions = new DbContextOptionsBuilder<CategoryDbContext>()
                .UseInMemoryDatabase(databaseName: $"in-memory-categories-test").EnableSensitiveDataLogging()
                .Options;

            try
            {
                DbContext = new CategoryDbContext(DbOptions);
                DbContext.AddRange(tags);
                DbContext.AddRange(categories);
                DbContext.SaveChanges();
            }
            catch (Exception)
            {
                DbContext.ChangeTracker.AcceptAllChanges();
            }
        }
    }
}
