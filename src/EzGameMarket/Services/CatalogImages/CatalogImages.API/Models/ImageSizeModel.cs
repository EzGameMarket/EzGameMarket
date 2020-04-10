using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CatalogImages.API.Models
{
    public class ImageSizeModel
    {
        [NotMapped]
        public const int MinWidth = 32;
        [NotMapped]
        public const int MaxWidth = 1024;
        [NotMapped]
        public const int MinHeight = 32;
        [NotMapped]
        public const int MaxHeight = 1024;

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public int Height { get; set; }

        public IEnumerable<CatalogItemImageModel> Images { get; set; }
    }
}