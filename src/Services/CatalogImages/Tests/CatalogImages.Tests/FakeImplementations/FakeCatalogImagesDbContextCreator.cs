using CatalogImages.API.Data;
using CatalogImages.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatalogImages.Tests.FakeImplementations
{
    public static class FakeCatalogImagesDbContextCreator
    {
        public static DbContextOptions CreateDbOptions(string dbName)
        {
            return new DbContextOptionsBuilder<CatalogImagesDbContext>()
                .UseInMemoryDatabase(databaseName: $"in-memory-catalog-images-test-{dbName}").EnableSensitiveDataLogging()
                .Options;
        }

        public static async Task InitDbContext(DbContextOptions options)
        {
            using (var dbContext = new CatalogImagesDbContext(options))
            {
                var sizes = CreateSizes();
                var types = CreateTypes();
                var imgs = CreateImages();
                imgs[0].Size = sizes[0];
                imgs[0].Type = types[0];
                imgs[1].Size = sizes[0];
                imgs[1].Type = types[0];
                imgs[2].Size = sizes[1];
                imgs[2].Type = types[1];

                sizes[0].Images = new List<CatalogItemImageModel>() { imgs[0], imgs[1] };
                types[0].Images = new List<CatalogItemImageModel>() { imgs[0], imgs[1] };

                sizes[1].Images = new List<CatalogItemImageModel>() { imgs[2] };
                types[1].Images = new List<CatalogItemImageModel>() { imgs[2] };

                await dbContext.AddRangeAsync(types);
                await dbContext.AddRangeAsync(sizes);
                await dbContext.AddRangeAsync(imgs);
                await dbContext.SaveChangesAsync();
            }
        }

        public static List<CatalogItemImageModel> CreateImages() => new List<CatalogItemImageModel>()
        {
            new CatalogItemImageModel()
            {
                ID = 1,
                ImageUri = "http://www.cdn.ezgame.hu/catalog/imgs/test.png",
                ProductID = "bfv",   
            },
            new CatalogItemImageModel()
            {
                ID = 2,
                ImageUri = "http://www.cdn.ezgame.hu/catalog/imgs/csgo1.png",
                ProductID = "csgo",
            },
            new CatalogItemImageModel()
            {
                ID = 3,
                ImageUri = "http://www.cdn.ezgame.hu/catalog/imgs/csgo2.png",
                ProductID = "csgo",
            },
        };

        public static List<ImageTypeModel> CreateTypes() => new List<ImageTypeModel>()
        {
            new ImageTypeModel()
            {
                ID = 1,
                Name = "Catalog"
            },
            new ImageTypeModel()
            {
                ID = 2,
                Name = "Product"
            }
        };

        public static List<ImageSizeModel> CreateSizes() => new List<ImageSizeModel>()
        {
            new ImageSizeModel()
            {
                ID = 1,
                Name = "Catalog",
                Height = 128,
                Width = 128
            },
            new ImageSizeModel()
            {
                ID = 2,
                Name = "Product",
                Width = 256,
                Height = 256
            }
        };
    }
}
