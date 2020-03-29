using System;
using System.Collections.Generic;
using System.Text;
using CatalogService.API.Data;
using CatalogService.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Tests.API.FakeImplementation
{
    public class FakeCatalogDbContext
    {
        internal List<Product> _products;
        List<Genre> genres = new List<Genre>()
            {
                new Genre()
                {
                    ID = 1,
                    Name = "FPS"
                },new Genre()
                {
                    ID = 2,
                    Name = "Akció"
                },new Genre()
                {
                    ID = 3,
                    Name = "Nem pay-to-win"
                },
            };
        List<Region> regions = new List<Region>()
            {
                new Region()
                {
                    ID = 1,
                    Name = "WW"
                },new Region()
                {
                    ID = 2,
                    Name = "RU"
                },new Region()
                {
                    ID = 3,
                    Name = "EU"
                }
            };
        List<Tag> tags = new List<Tag>()
            {
                new Tag()
                {
                    ID = 1,
                    Name = "FPS"
                },new Tag()
                {
                    ID = 2,
                    Name = "FTP"
                },new Tag()
                {
                    ID = 3,
                    Name = "RPG"
                },new Tag()
                {
                    ID = 4,
                    Name = "PTW"
                },new Tag()
                {
                    ID = 5,
                    Name = "AKCIÓ"
                },
                new Tag()
                {
                    ID = 6,
                    Name = "CSGO"
                },
                new Tag()
                {
                    ID = 7,
                    Name = "STEAM"
                },
            };
        List<Platform> platforms = new List<Platform>()
            {
                new Platform()
                {
                    ID = 1,
                    Name = "STEAM"
                },new Platform()
                {
                    ID = 2,
                    Name = "Origin"
                },new Platform()
                {
                    ID = 3,
                    Name = "UPlay"
                },
            };
        List<Language> langs = new List<Language>()
            {
                new Language()
                {
                    ID = 1,
                    Name = "EN"
                },new Language()
                {
                    ID = 2,
                    Name = "HU"
                },new Language()
                {
                    ID = 3,
                    Name = "DE"
                },new Language()
                {
                    ID = 4,
                    Name = "RU"
                },
            };
        List<ImgSize> imgSizes = new List<ImgSize>()
            {
                new ImgSize()
                {
                    ID = 1,
                    Size = "Catalog"
                },new ImgSize()
                {
                    ID = 2,
                    Size = "Detail"
                }
            };

        public ProductDbContext DBContext { get; set; }
        public DbContextOptions<ProductDbContext> DbOptions { get; set; }

        public FakeCatalogDbContext()
        {
            _products = new List<Product>()
            {
                new Product()
                {
                    ID = "1",
                    GameID = "csgo",
                    Description = "A Counter Strike Global Offensive az eddig legjobb cs",
                    DiscountedPrice = 0,
                    Price = 3000,
                    Genres = genres,
                    Images = new List<ProductImage>()
                    {
                        new ProductImage()
                        {
                            ID = 1,
                            ProductID = "1",
                            SizeID = 1,
                            Size = imgSizes[0],
                            Url = "csgo_catalog.jpg"
                        },new ProductImage()
                        {
                            ID = 3,
                            ProductID = "1",
                            SizeID = 2,
                            Size = imgSizes[1],
                            Url = $"csgo_1.jpg"
                        },new ProductImage()
                        {
                            ID = 7,
                            ProductID = "1",
                            SizeID = 2,
                            Size = imgSizes[1],
                            Url = "csgo_2.jpg"
                        },new ProductImage()
                        {
                            ID = 8,
                            ProductID = "1",
                            SizeID = 2,
                            Size = imgSizes[1],
                            Url = "csgo_3.jpg"
                        }
                    },
                    Languages = new List<Language>()
                    {
                        langs[0],
                        langs[2]
                    },
                    Name = "Counter Strike Global Offensive",
                    Platforms = new List<Platform>() {platforms[0]},
                    Regions = new List<Region>() { regions[1]},
                    Tags = new List<Tag>() {tags[0],tags[1],tags[3],tags[4]},
                    RelaseDate = new DateTime(2012,3,12)
                },
                new Product()
                {
                    ID = "2",
                    GameID = "gtav",
                    Description = "A GTA V a legjobb gta",
                    DiscountedPrice = 5000,
                    Price = 10000,
                    Genres = new List<Genre>() {genres[0],genres[1] },
                    Images = new List<ProductImage>()
                    {
                        new ProductImage()
                        {
                            ID = 10,
                            ProductID = "2",
                            SizeID = 1,
                            Size = imgSizes[0],
                            Url = "gtav_catalog.jpg"
                        },new ProductImage()
                        {
                            ID = 2,
                            ProductID = "2",
                            SizeID = 2,
                            Size = imgSizes[1],
                            Url = $"gtav_1.jpg"
                        },new ProductImage()
                        {
                            ID = 4,
                            ProductID = "2",
                            SizeID = 2,
                            Size = imgSizes[1],
                            Url = "gtav_2.jpg"
                        }
                    },
                    Languages = new List<Language>()
                    {
                        langs[0],
                        langs[1],
                        langs[3]
                    },
                    Name = "Grand Theft Auto V",
                    Platforms = new List<Platform>() {platforms[0]},
                    Regions = new List<Region>() { regions[1]},
                    Tags = new List<Tag>() {tags[0]},
                    RelaseDate = new DateTime(2010,3,12)
                },
                new Product()
                {
                    ID = "3",
                    GameID = "bfv",
                    Description = "A BFV a legújabb BF",
                    DiscountedPrice = 15000,
                    Price = 23000,
                    Genres = genres,
                    Images = new List<ProductImage>()
                    {
                        new ProductImage()
                        {
                            ID = 5,
                            ProductID = "3",
                            SizeID = 1,
                            Size = imgSizes[0],
                            Url = "bfv_catalog.jpg"
                        },new ProductImage()
                        {
                            ID = 6,
                            ProductID = "3",
                            SizeID = 2,
                            Size = imgSizes[1],
                            Url = $"bfv_1.jpg"
                        }
                    },
                    Languages = new List<Language>()
                    {
                        langs[0],
                        langs[1]
                    },
                    Name = "BattleField V",
                    Platforms = new List<Platform>() {platforms[1]},
                    Regions = new List<Region>() { regions[1]},
                    Tags = new List<Tag>() {tags[0],tags[2],tags[3],tags[5]},
                    RelaseDate = new DateTime(2019,10,12)
                }
            };


            DbOptions = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(databaseName: $"in-memory-catalog-test{Guid.NewGuid()}")
                .Options;

            try
            {
                DBContext = new ProductDbContext(DbOptions);
                DBContext.AddRange(_products);
                DBContext.SaveChanges();
            }
            catch (Exception)
            {

            }
        }

        public void Set30CatalogItems()
        {
            var product = new Product()
            {
                ID = "6",
                GameID = "bfi",
                Description = "A BFV a legújabb BF",
                DiscountedPrice = 15000,
                Price = 23000,
                Genres = genres,
                Images = new List<ProductImage>()
                    {
                        new ProductImage()
                        {
                            ID = 20,
                            ProductID = "3",
                            SizeID = 1,
                            Size = imgSizes[0],
                            Url = "bfv_catalog.jpg"
                        },new ProductImage()
                        {
                            ID = 22,
                            ProductID = "3",
                            SizeID = 2,
                            Size = imgSizes[1],
                            Url = $"bfv_1.jpg"
                        }
                    },
                Languages = new List<Language>()
                    {
                        langs[0],
                        langs[1]
                    },
                Name = "BattleField V",
                Platforms = new List<Platform>() { platforms[1] },
                Regions = new List<Region>() { regions[1] },
                Tags = new List<Tag>() { tags[0], tags[2], tags[3] },
                RelaseDate = new DateTime(2019, 10, 12)
            };

            _products = new List<Product>()
            {
                new Product()
                {
                    ID = "1",
                    GameID = "csgo",
                    Description = "A Counter Strike Global Offensive az eddig legjobb cs",
                    DiscountedPrice = 0,
                    Price = 3000,
                    Genres = genres,
                    Images = new List<ProductImage>()
                    {
                        new ProductImage()
                        {
                            ID = 1,
                            ProductID = "1",
                            SizeID = 1,
                            Size = imgSizes[0],
                            Url = "csgo_catalog.jpg"
                        },new ProductImage()
                        {
                            ID = 3,
                            ProductID = "1",
                            SizeID = 2,
                            Size = imgSizes[1],
                            Url = $"csgo_1.jpg"
                        },new ProductImage()
                        {
                            ID = 7,
                            ProductID = "1",
                            SizeID = 2,
                            Size = imgSizes[1],
                            Url = "csgo_2.jpg"
                        },new ProductImage()
                        {
                            ID = 8,
                            ProductID = "1",
                            SizeID = 2,
                            Size = imgSizes[1],
                            Url = "csgo_3.jpg"
                        }
                    },
                    Languages = new List<Language>()
                    {
                        langs[0],
                        langs[2]
                    },
                    Name = "Counter Strike Global Offensive",
                    Platforms = new List<Platform>() {platforms[0]},
                    Regions = new List<Region>() { regions[1]},
                    Tags = new List<Tag>() {tags[0],tags[1],tags[3],tags[4]},
                    RelaseDate = new DateTime(2012,3,12)
                },
                new Product()
                {
                    ID = "2",
                    GameID = "gtav",
                    Description = "A GTA V a legjobb gta",
                    DiscountedPrice = 5000,
                    Price = 10000,
                    Genres = new List<Genre>() {genres[0],genres[1] },
                    Images = new List<ProductImage>()
                    {
                        new ProductImage()
                        {
                            ID = 1,
                            ProductID = "2",
                            SizeID = 1,
                            Size = imgSizes[0],
                            Url = "gtav_catalog.jpg"
                        },new ProductImage()
                        {
                            ID = 2,
                            ProductID = "2",
                            SizeID = 2,
                            Size = imgSizes[1],
                            Url = $"gtav_1.jpg"
                        },new ProductImage()
                        {
                            ID = 4,
                            ProductID = "2",
                            SizeID = 2,
                            Size = imgSizes[1],
                            Url = "gtav_2.jpg"
                        }
                    },
                    Languages = new List<Language>()
                    {
                        langs[0],
                        langs[1],
                        langs[3]
                    },
                    Name = "Grand Theft Auto V",
                    Platforms = new List<Platform>() {platforms[0]},
                    Regions = new List<Region>() { regions[1]},
                    Tags = new List<Tag>() {tags[0]},
                    RelaseDate = new DateTime(2010,3,12)
                },
                new Product()
                {
                    ID = "3",
                    GameID = "bfv",
                    Description = "A BFV a legújabb BF",
                    DiscountedPrice = 15000,
                    Price = 23000,
                    Genres = genres,
                    Images = new List<ProductImage>()
                    {
                        new ProductImage()
                        {
                            ID = 5,
                            ProductID = "3",
                            SizeID = 1,
                            Size = imgSizes[0],
                            Url = "bfv_catalog.jpg"
                        },new ProductImage()
                        {
                            ID = 6,
                            ProductID = "3",
                            SizeID = 2,
                            Size = imgSizes[1],
                            Url = $"bfv_1.jpg"
                        }
                    },
                    Languages = new List<Language>()
                    {
                        langs[0],
                        langs[1]
                    },
                    Name = "BattleField V",
                    Platforms = new List<Platform>() {platforms[1]},
                    Regions = new List<Region>() { regions[1]},
                    Tags = new List<Tag>() {tags[0],tags[2],tags[3]},
                    RelaseDate = new DateTime(2019,10,12)
                },
                product,
                product,
                product,
                product,
                product,
                product,
                product,
                product,
                product,
                product,
                product,
                product,
                product,
                product,
                product,
                product,
                product,
                product,
                product,
                product,
                product,
                product,
                product,
                product,
                product,
                product,
                product,

            };

            DBContext = new ProductDbContext(DbOptions);
            DBContext.AddRange(_products);
            DBContext.SaveChanges();
        }

        public void DeleteCatalog()
        {
            DBContext = new ProductDbContext(DbOptions);
            DBContext.RemoveRange(_products);
            DBContext.SaveChanges();

            _products = new List<Product>();
        }
    }
}
