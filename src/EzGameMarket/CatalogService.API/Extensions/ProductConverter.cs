using CatalogService.API.Models;
using CatalogService.API.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.Extensions
{
    public static class ProductConverter
    {
        static string GetImageForCatalogItem(Product p)
        {
            var output = "Default.jpg";

            var fstImg = p.Images.FirstOrDefault(i => i.Size.Size == "Catalog");

            if (fstImg != default)
            {
                output = fstImg.Url;
            }

            return output;
        }
        static string GetGenreForCatalogItem(Product p)
        {
            var output = "Ismeretlen";

            var fstGenre = p.Genres.FirstOrDefault();

            if (fstGenre != default)
            {
                output = fstGenre.Name;
            }

            return output;
        }

        public static CatalogItem ToCatalogItem(this Product p) => new CatalogItem(
            GetImageForCatalogItem(p),
            p.GameID,
            p.Name,
            p.Price,
            p.DiscountedPrice,
            GetGenreForCatalogItem(p));

    }
}
