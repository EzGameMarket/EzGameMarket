using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CatalogService.API.Models
{
    public class Product
    {
        [Required]
        [Key]
        public string ID { get; set; }

        [Required]
        public string GameID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int DiscountedPrice { get; set; }

        [Required]
        public DateTime RelaseDate { get; set; }

        public List<Language> Languages { get; set; }
        public List<Platform> Platforms { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Region> Regions { get; set; }
        public List<ProductImage> Images { get; set; }
    }
}