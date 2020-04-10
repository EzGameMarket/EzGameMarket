using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogImages.API.Models
{
    public class ImageTypeModel
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<CatalogItemImageModel> Images { get; set; }
    }
}